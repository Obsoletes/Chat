using System;
using System.Collections.Generic;
using System.Text;
#nullable enable
namespace Server
{
	public class SocketRequest
	{
		public SocketRequest(User user, byte[] body, int pos)
		{
			this.Body = body;
			this.User = user;
			this.Pos = pos;
		}
		public User User
		{
			get;
			set;
		}
		public byte[] Body
		{
			get;
			set;
		}
		public int Pos
		{
			get;
			set;
		}
	}
	public class SocketRespond
	{
		public unsafe SocketRespond(User user, byte[] body, int length)
		{
			this.Body = body;
			this.User = user;
			this.Length = length;
		}
		public User User
		{
			get;
			set;
		}
		public byte[] Body
		{
			get;
			set;
		}
		public int Length
		{
			get;
			set;
		}
	}
	interface ISocketHandle
	{
		SocketRespond Handle(SocketRequest request);
	}
}
