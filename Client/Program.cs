using System;
#nullable enable
namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			using (Client client = new Client("test"))
			{
				client.Connect(6000);
				while(true)
				{
					Console.WriteLine("1 for query\n2 for enter\n3 for echo");
					switch (Console.ReadLine())
					{
						case "1":
							foreach (var ob in client.QueryUser())
							{
								Console.WriteLine(ob);
							}
							break;
						case "2":
							Console.WriteLine(client.Hello());
							break;
						case "3":
							string str = Console.ReadLine();
							Console.WriteLine(client.Echo(str));
							break;
					}
				}
			}
			Console.ReadLine();
		}
	}
}
