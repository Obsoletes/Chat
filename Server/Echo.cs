using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
	[SocketHandle(Prefix = "echo")]
	public class Echo : ISocketHandle
	{
		SocketRespond ISocketHandle.Handle(SocketRequest request)
		{
			string message = AnalysisEchoString(request.Body, request.Pos, request.Length);
			byte[] bs = Encoding.UTF8.GetBytes(message);
			return new SocketRespond(request.User, bs, bs.Length);
		}
		private unsafe string AnalysisEchoString(byte[] body,int pos,int length)
		{
			fixed(byte* bs=body)
			{
				byte* messageBegin = bs + pos;
				return Encoding.UTF8.GetString(messageBegin, length - pos);
			}
		}
	}
}
