using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Client
{
	class Client : IDisposable
	{
		public string Name
		{
			get;
			private set;
		}
		public Client(string name)
		{
			this.Name = name;
		}
		public void Connect(int port)
		{
			string host = "47.100.172.185";
			IPAddress ip = IPAddress.Parse(host);
			IPEndPoint ipe = new IPEndPoint(ip, port);
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			socket.Connect(ipe);
			socket.Send(Encoding.UTF8.GetBytes("E" + Name));
		}
		public string Hello()
		{
			socket.Send(Encoding.UTF8.GetBytes("enter"));
			byte[] recBytes = new byte[4096];
			int bytes = socket.Receive(recBytes, recBytes.Length, 0);
			return Encoding.UTF8.GetString(recBytes);
		}
		public IEnumerable<string> QueryUser()
		{
			socket.Send(Encoding.UTF8.GetBytes("query"));
			byte[] recBytes = new byte[4096];
			int bytes = socket.Receive(recBytes, recBytes.Length, 0);
			Console.Error.WriteLine(bytes);
			return AnalysisQueryUserResult(recBytes, bytes);
		}
		public void Dispose()
		{
			((IDisposable)socket)?.Dispose();
		}
		private unsafe IEnumerable<string> AnalysisQueryUserResult(byte[] bys, int length)
		{
			List<string> users = new List<string>();
			fixed (byte* ptr = bys)
			{
				byte* end = ptr + length;
				byte* start = ptr;
				while (start <= end)
				{
					byte nameLength = *start;
					start += 1;
					if (start + nameLength <= end)
						users.Add(Encoding.UTF8.GetString(start, nameLength));
					start += nameLength;
				}
			}
			return users;
		}
		private Socket socket;
	}
}
