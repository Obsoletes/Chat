using System;
using System.Collections.Generic;
using System.Text;
#nullable enable
namespace Server
{
	public class Trie<T> where T : class
	{
		public Trie()
		{
			root = new Node();
		}
		public void Insert(string str, T value)
		{
			InsertImpl(str, value, 0, root);
		}
		public T? Find(string str)
		{
			return FindImpl(str, 0, root);
		}
		private T? FindImpl(string str, int index, Node node)
		{
			if (index == str.Length - 1)
			{
				if (node.Value != null)
				{
					return node.Value;
				}
			}
			else
			{
				char c = str[index];
				foreach (Node next in node.Next)
				{
					if (next.Word == c)
					{
						return FindImpl(str, index + 1, next);
					}
				}
			}
			return null;
		}
		private void InsertImpl(string str, T value, int index, Node node)
		{
			if (index == str.Length - 1)
			{
				node.Value = value;
			}
			else
			{
				char c = str[index];
				Node? nextJmp = null;
				foreach (Node next in node.Next)
				{
					if (next.Word == c)
					{
						nextJmp = next;
						break;
					}
				}
				if (nextJmp == null)
				{
					nextJmp = new Node { Word = c };
					node.Next.Add(nextJmp);
				}
				InsertImpl(str, value, index + 1, nextJmp);
			}
		}
		private class Node
		{
			public T? Value
			{
				get;
				set;
			}
			public char Word
			{
				get;
				set;
			}
			public List<Node> Next
			{
				get;
				set;
			} = new List<Node>();
		}
		private Node root;
	}
}
