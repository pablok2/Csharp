/*
 * List class: basic class, with an added feature to insert an item
 * in the correct order, allowing to hide the implementation. It holds 
 * a private Node class that keeps track of the count of items, keeping the list smaller
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace Program1
{
	public class List<T> : IEnumerable
	{
		//private class to hold the data info
		public class Node
		{
			public T item;
			int _count;
			public Node next;

			public int Count { get { return _count;} set { _count = value;} }
		}

		//empty head node, handle to the list
		Node  head;
		private int _nodeCount;
		public int nodeCount { get { return _nodeCount; } set { _nodeCount = value; } }

		//Start an empty list
		public List ()
		{
			head = new Node();
			head.next = null;
			head.Count = 0;
			nodeCount = 0;
		}

		//Add an element, create a new node, add in front
		public void Add (T item)
		{
			Node current = new Node ();

			if (this.head.next != null) {
				current = this.head.next;

				while (current.next != null) {
					current = current.next;
					if (current.item.Equals(item) ) {
						current.Count++;
						nodeCount++;
						return;
					}
				}
			}

			//Didn't find one existing already, so create one and add
			Node newNode = new Node();
			newNode.item = item;
			newNode.Count = 1;;
			newNode.next = null;

			newNode.next = this.head.next;
			this.head.next = newNode;
			nodeCount++;
		}

		//returns the node identified by it's item
		public Node GetNode (T item)
		{
			if ( (this.head.next == null) ||  (!Contains (item) ) )
			{
				Console.WriteLine ("Node does not exist.");
				return new Node ();
			}
				
			Node current = new Node ();
			current = this.head;

			while (current.next != null) {
				current = current.next;
				if (current.item.Equals (item)) 
				{
					return current;
				}
			}
			return new Node();
		}

		//Adds a node directly after the Head node
		public void AddNode (Node node)
		{
			Node current = new Node ();
			current = this.head;

			node.next = current.next;
			current.next = node;
		}

		//The usual expectation, return true if found
		public bool Contains (T item)
		{
			Node current = new Node ();
			bool found = false;

			if (this.head.next != null) {
				current = this.head.next;

				while (!found) {
					if (current.item.Equals (item)) {
						return true;					}

					if (current.next == null) { found = true; }
					else { current = current.next; }
				}
			}
			return false;
		}

		//This will get the count of items by scrolling through the list
		public int GetItemCount (T item)
		{
			if (!Contains (item)) {
				Console.WriteLine ("This item is not in the Collection.");
				return 0;
			}
			Node current = new Node ();
			bool found = false;

			if (this.head.next != null) {
				current = this.head.next;

				while (!found) {
					if (current.item.Equals (item)) { return current.Count;}

					else { 
						if (current.next == null) found = true;
						else current = current.next; 
					}
				}
			}
			return 0;
		}

		//Removes said item, decrement count or remove node is last one
		public void Remove (T item)
		{
			if (!Contains (item)) {
				Console.WriteLine ("This item is not in the Collection.");
				return;
			}

			Node current = new Node ();
			Node temp    = new Node ();
			bool found   = false;

			if (this.head.next != null) {
				temp 	= this.head;
				current = temp.next;

				while (!found) {
					if (current.item.Equals (item)) 
					{
						found = true;
						if (current.Count > 1) 
						{
							current.Count --;
							return;
						}
						//Last member, delete the whole node
						else
						{
							temp.next = current.next;
							current.next = null;
							current.Count = 0;
							current.item = default(T);
							return;
						}
					}

					if (current.next != null) 
					{
						temp = current;
						current = temp.next;
					}
				}
			}
			nodeCount--;
			return;
		}

		//Delete a single node based on teh item
		public void DeleteNode (T item)
		{
			if (!Contains (item)) {
				Console.WriteLine ("This item is not in the Collection.");
				return;
			}

			Node current = new Node ();
			Node temp    = new Node ();

			if (this.head.next != null) {
				temp 	= this.head;
				current = temp;

				while (current.next != null) {
					current = current.next;

					if (current.item.Equals (item)) 
					{
						nodeCount -= current.Count;
						temp.next = current.next;
						current.next = null;
						current.Count = 0;
						current.item = default(T);
						return;
					}
					temp = current;
				}
			}
			return;
		}

		//Custom ToString method to print the whole list
		public void toString ()
		{
			Node current = new Node(); 
			current = this.head.next;

			while (current != null) {
				Console.WriteLine ("Item: " + current.item.ToString()
					+ ": " + current.Count + " times."
				);
				if (current.next == null) {
					return;
				}
				current = current.next;
			}
		}

		//Copies on e list onto another, one by one
		public List<T> CopyFrom (List<T> copy)
		{
			Node current = new Node ();
			Node currCopy = new Node();

			current = this.head;
			currCopy = copy.head;

			while (currCopy.next != null) {

				Node temp = new Node ();

				currCopy = currCopy.next;

				temp.item = currCopy.item;
				temp.Count = currCopy.Count;
				temp.next = currCopy.next;

				temp.next = current.next;
				current.next = temp;
			}
			return this;
		}

		//This will add a node to the list in proper order
		public void AddOrgNode (T item)
		{

			if (Contains (item)) {
				Node node = new Node ();
				node = GetNode (item);
				node.Count++;
				return;
			}
			Node itemNode = new Node();

			Node current = new Node ();
			Node prev = new Node ();

			var tX = item as IComparable;

			itemNode.item = item;
			itemNode.Count=1;

			current = this.head;
			prev = current;

			while (current.next != null) 
			{
				current = prev.next;

				var currentX = current.item as IComparable;

				if (tX.CompareTo(currentX) < 1 )
				{
					itemNode.next = prev.next;
					prev.next = itemNode;
					return;
				}
				prev = current;
			}

			itemNode.next = current.next;
			current.next = itemNode;
			return;
		}

		//Check if empty
		public bool IsEmpty ()
		{
			if (head.next == null) return true;
			else return false;
		}
	#region IEnumerable<T> Members
    public IEnumerator GetEnumerator()
    {
		Node current = new Node();
		current = head;

		while (current.next != null)
		{
			current = current.next;
			yield return current.item;
		}
    }
    #endregion
    #region IEnumerable Members

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        // Lets call the generic version here
        return this.GetEnumerator();
    }
    #endregion
	}
}//namespace
