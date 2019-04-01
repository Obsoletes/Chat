using System;

namespace Server
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	sealed class SocketHandleAttribute : Attribute
	{
		public string Prefix { get; set; }
	}
}
