using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
namespace Server
{
	class Server : IDisposable
	{
		public Server()
		{
			users = new List<string> { "123", "456", "79", "123", "456", "79", "123", "456", "79" };
		}
		public void Listen(int port)
		{
			IPEndPoint ipe = new IPEndPoint(IPAddress.Any, port);
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Bind(ipe);
			socket.Listen(0);
			byte[] recByte = new byte[512];
			while (true)
			{
				Socket serverSocket = socket.Accept();
				int bytes = serverSocket.Receive(recByte, recByte.Length, 0);
				Task.Run(() => Loop(serverSocket), CancellationToken.None);
			}
		}

		public void Dispose()
		{
			((IDisposable)socket)?.Dispose();
		}
		private void Loop(Socket socket)
		{
			byte[] recByte = new byte[512];
			int bytes = socket.Receive(recByte, recByte.Length, 0);
			if (recByte[0] == 'Q')
			{
				WriteQueryUserResult(recByte);
				socket.Send(recByte);
			}
		}
		private unsafe void WriteQueryUserResult(byte[] bys)
		{
			fixed (byte* ptr = bys)
			{
				byte* start = ptr;
				foreach (string user in users)
				{
					byte[] tmp = Encoding.UTF8.GetBytes(user);
					*start = (byte)tmp.Length;
					start += 1;
					for (int i = 0; i < tmp.Length; i++)
					{
						*start = tmp[i];
						start += 1;
					}
				}
			}
		}
		private byte[] echoBys = Encoding.UTF8.GetBytes("<echo>");
		private Socket socket;
		private List<string> users;
	}
}
