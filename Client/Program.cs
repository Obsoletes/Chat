using System;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			using (Client client = new Client("test"))
			{
				client.Connect(8000);
				client.QueryUser();
				foreach (string user in client.QueryUser())
				{
					Console.WriteLine(user);
				}
			}
			Console.ReadLine();
		}
	}
}
