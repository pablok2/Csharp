using System;
using System.Collections;
using System.Collections.Generic;

namespace Program1
{
	public class OrderedBag<T> : Bag<T>, OrderedCollection<T> where T : IComparable
	{
		List<T> orderItemBag;

		//Constructors are treated as static for inherited classes
		public OrderedBag () 
		{ 
			orderItemBag = new List<T>();
		}

		//Add an item in order
		public void insert (T item)
		{
			orderItemBag.AddOrgNode(item);	
		}

		//UNion will take all members, and the highest number of elements if each list
		//contains the same element
		public void Union (OrderedBag<T> AList)
		{
			List<T> finalList = new List<T> ();
			List<T> mergeList = new List<T> ();

			finalList.CopyFrom (this.orderItemBag );
			mergeList.CopyFrom (AList.orderItemBag);

			//Idea is to copy one list, then scroll the other, find lowest number
			//and use that, then delete it, then take remaing members and add them
			foreach (T t in finalList) {

				if (mergeList.Contains(t) )
				{
					if (finalList.GetItemCount(t) >= mergeList.GetItemCount(t) )
					{
						mergeList.Remove(t);
					}
					else
					{
						finalList.Remove (t);
						finalList.AddNode(mergeList.GetNode (t) );
						mergeList.Remove(t);
					}
				}
				else
				{
					finalList.Remove (t);
				}
			}

			if (mergeList.nodeCount > 0 )
				foreach (T t in mergeList) 
					finalList.AddNode( mergeList.GetNode(t) );
						
			Console.WriteLine ("Union of the lists is:");
			finalList.toString();
		}


		//Returns a List which represents the Intersection of two Lists
		public void Intersection (OrderedBag<T> ABag)
		{
			List<T> finalList = new List<T> ();
			List<T> thisList = new List<T> ();
			List<T> mergeList = new List<T> ();

			thisList.CopyFrom (orderItemBag);
			mergeList.CopyFrom (ABag.orderItemBag);

			//Idea is to copy one list, then scroll the other, find lowest number
			//and use that, then delete it, then take remaing members and add them
			foreach (T t in thisList) {

				if (mergeList.Contains(t) )
				{
					if (thisList.GetItemCount(t) < mergeList.GetItemCount(t) )
					{ 
						finalList.AddNode(orderItemBag.GetNode(t));
					}

					else 
					{ 
						finalList.AddNode(ABag.orderItemBag.GetNode(t)); 
					}
				}
			}
			Console.WriteLine ("Intersection of the lists is:");
			finalList.toString();
		}

		//Checks if the Bag is ordered or not
		public bool isOrdered ()
		{
			T current = default(T);
			bool isFirst = true;

			foreach (T t in orderItemBag) 
			{
				if (isFirst)
				{
					current = t;
					isFirst = false;
				}
				else
				{
					var currentX = current as IComparable;
					var tX = t as IComparable;

					if ( (current == null) || (tX.CompareTo(currentX) > 0) ) return false;
				}
				current = t;
			}
			return true;
		}

		public void displayBag ()
		{
			foreach (T t in orderItemBag)
			{
				Console.WriteLine ( t.ToString() + ":  " +( orderItemBag.GetItemCount(t) ) );
			}
		}
	}
}
