using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Runing");
			Server server = new Server();
			server.Listen(8000);
        }
    }
}
