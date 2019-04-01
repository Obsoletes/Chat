using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Server
{
	[SocketHandle(Prefix = "query")]
	public class QueryRoom : ISocketHandle
	{
		SocketRespond ISocketHandle.Handle(SocketRequest request)
		{
			byte[] body = new byte[512];
			SocketRespond respond = new SocketRespond(request.User, body, 0)
			{
				Length = WriteQueryUserResult(body, request.User.Users, request.User)
			};
			return respond;
		}
		private unsafe int WriteQueryUserResult(byte[] bys, IEnumerable<User> users, User user)
		{
			fixed (byte* ptr = bys)
			{
				byte* start = ptr;
				foreach (var o in users.Where(u => u != user))
				{
					byte[] tmp = Encoding.UTF8.GetBytes(o.Name);
					*start = (byte)tmp.Length;
					start += 1;
					for (int i = 0; i < tmp.Length; i++)
					{
						*start = tmp[i];
						start += 1;
					}
				}
				return (int)(start - ptr);
			}
		}
	}
}
