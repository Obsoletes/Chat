using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
	[SocketHandle(Prefix = "enter")]
	class EnterRoom : ISocketHandle
	{
		public SocketRespond Handle(SocketRequest request)
		{
			byte[] bys = Encoding.UTF8.GetBytes("Hello " + request.User.Name);
			SocketRespond respond = new SocketRespond(request.User, bys, bys.Length);
			return respond;
		}
	}
}
