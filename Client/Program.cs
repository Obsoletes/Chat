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
					Console.WriteLine("1 for query\n2for enter");
					switch (Console.ReadLine())
					{
						case "1":
							foreach (var ob in client.QueryUser())
							{
								Console.WriteLine(ob);
							}
							break;
						case "2":

							break;
					}
				}
			}
			Console.ReadLine();
		}
	}
}
