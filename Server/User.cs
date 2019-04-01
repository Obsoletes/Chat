using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
#nullable enable
namespace Server
{
	public class User
	{
		public User(string name, Socket socket, List<User> users)
		{
			this.Name = name;
			this.Socket = socket;
			this.Users = users;
		}
		public List<User> Users
		{
			get; set;
		}
		public string Name
		{
			get;
			set;
		}
		public Socket Socket
		{
			get;
			set;
		}
	}
}
