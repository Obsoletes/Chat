using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Runing");
			Server server = new Server();
			server.FindSocketHandle(typeof(Program).Assembly);
			server.Listen(6000);
			/*Trie<string> trie = new Trie<string>();
			trie.Insert("123", "a");
			trie.Insert("123", "b");
			trie.Insert("123", "c");
			trie.Insert("123", "d");
			trie.Insert("123", "e");
			trie.Insert("123", "f");
			Console.WriteLine(trie.Find("123") ?? "Not found");*/
			Console.ReadLine();
		}
    }
}
