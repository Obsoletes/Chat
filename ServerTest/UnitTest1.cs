using Microsoft.VisualStudio.TestTools.UnitTesting;
#nullable enable
namespace ServerTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TrieTest()
		{
			Server.Trie<string> trie = new Server.Trie<string>();
			//trie.Insert(null, "123");
			trie.Insert("1", "data_1");
			trie.Insert("12", "data_12");
			trie.Insert("123", "data_123");
			trie.Insert("321", "datar_321");
			trie.Insert("21", "datar_21");
			trie.Insert("1", "datar_1");
			Assert.AreEqual(trie.Find("321"),"datar_321");
			Assert.AreNotEqual(trie.Find("1"), "data_1");
		}
	}
}
