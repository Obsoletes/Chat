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
			socket.Send(Encoding.UTF8.GetBytes("<echo>"));
		}
		public IEnumerable<string> QueryUser()
		{
			socket.Send(Encoding.UTF8.GetBytes("Q"));
			byte[] recBytes = new byte[4096];
			int bytes = socket.Receive(recBytes, recBytes.Length, 0);
			return AnalysisQueryUserResult(recBytes);
		}
		public void Dispose()
		{
			((IDisposable)socket)?.Dispose();
		}
		private unsafe IEnumerable<string> AnalysisQueryUserResult(byte[] bys)
		{
			List<string> users = new List<string>();
			fixed (byte* ptr = bys)
			{
				byte* end = ptr + bys.Length;
				byte* start = ptr;
				while (start <= end)
				{
					byte nameLength = *ptr;
					start += 1;
					if (start + nameLength <= end)
						users.Add(Encoding.UTF8.GetString(start, nameLength));
				}
			}
			return users;
		}
		private Socket socket;
	}
}
