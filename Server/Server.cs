using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
#nullable enable
namespace Server
{
	class Server
	{
		public Server()
		{
			users = new List<User>();
			handles = new Trie<ISocketHandle>();
		}
		public void Listen(int port)
		{
			IPEndPoint ipe = new IPEndPoint(IPAddress.Any, port);
			using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				socket.Bind(ipe);
				socket.Listen(0);
				byte[] recByte = new byte[512];
				while (true)
				{
					Socket userSocket = socket.Accept();
					if (TryVerify(userSocket, out string name))
					{
						Task.Run(() => Loop(userSocket, name), CancellationToken.None);
					}
					else
					{
						userSocket.Shutdown(SocketShutdown.Both);
					}
				}
			}
		}

		private bool TryVerify(Socket socket, out string name)
		{
			byte[] recByte = new byte[128];
			int length = socket.Receive(recByte);
			if (recByte[0] == 'E')
			{
				name = Encoding.UTF8.GetString(recByte, 1, length - 1);
				return true;
			}
			name = string.Empty;
			return false;
		}
		public void FindSocketHandle(Assembly assembly)
		{
			foreach (Type type in assembly.GetTypes())
			{
				SocketHandleAttribute attribute = type.GetCustomAttribute<SocketHandleAttribute>();
				if (attribute != null)
				{
					object ob = type.GetConstructor(new Type[] { }).Invoke(new object[] { });
					if (ob is ISocketHandle handle)
					{
						handles.Insert(attribute.Prefix, handle);
					}
				}
			}
		}
		private void Loop(Socket socket, string name)
		{
			User user = new User(name, socket, users);
			byte[] recByte = new byte[512];
			try
			{
				while (true)
				{
					int bytes = socket.Receive(recByte, recByte.Length, 0);
					int pos = 0;
					for (; pos < bytes; ++pos)
					{
						if (recByte[pos] == '\0')
							break;
					}
					string keyString = Encoding.UTF8.GetString(recByte, 0, pos);
					Console.WriteLine("Do Action {0}", keyString);
					SocketRespond? respond = SendRequest(handles.Find(keyString), user, recByte, bytes, pos);
					if (respond != null)
					{
						socket.Send(respond.Body, respond.Length, SocketFlags.None);
					}
				}
			}
			catch (SocketException ex)
			{
				if (socket.Connected == true)
				{
					socket.Shutdown(SocketShutdown.Both);
				}
				Console.WriteLine(ex.StackTrace);
			}
		}

		private unsafe SocketRespond? SendRequest(ISocketHandle? handle, User user, byte[] body, int length, int pos)
		{
			return handle?.Handle(new SocketRequest(user, body, pos, length));
		}
		private Trie<ISocketHandle> handles;
		private List<User> users;
	}
}
