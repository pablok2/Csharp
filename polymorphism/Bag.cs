using System;

namespace Program1
{
	public class Bag<T> : Collection<T>
	{
		List<T> itemBag;

		//Constructor instantiates a new List
		public Bag ()
		{
			itemBag = new List<T>();
		}

		//Inserts one item into the Collection at the end, unsorted
		public bool insert (T item)
		{
			try
			{
				itemBag.Add(item);
			} catch(Exception e){
				Console.Write("Cannot add item : " + e.ToString() );
				return false;
			}
			return true;
		}

		//Deletes one item from the Collection
		public bool delete (T item)
		{
			if (Contains(item)) 
			{
				try {
					itemBag.Remove (item);
				} catch (Exception e) {
					Console.Write ("Cannot remove item : " + e.ToString ());
					return false;
				}
			} else {
				Console.WriteLine ("This item does not exist in the Collection: " + item.ToString() );
				return false;
			}
			return true;
		}

		//Test to see if there is at least one identical member in the Collection
		public bool Contains (T item){ return itemBag.Contains(item);	}
		//Test of equality between two objects
		public bool isEqual 	(T item1, T item2){ return (item1.Equals(item2) ); }

		//Counts all the items in the collection that are equal to it
		public int countItem (T item) { return itemBag.GetItemCount(item); }

		//Returns the Union of two Lists passed to the Method
		public void Union (Bag<T> AList)
		{
			List<T> finalList = new List<T> ();
			List<T> mergeList = new List<T> ();

			finalList.CopyFrom (this.getList ());
			mergeList.CopyFrom (AList.getList() );

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
		public void Intersection (Bag<T> ABag)
		{
			List<T> finalList = new List<T> ();
			List<T> thisList = new List<T> ();
			List<T> mergeList = new List<T> ();

			thisList.CopyFrom (this.getList ());
			mergeList.CopyFrom (ABag.getList() );

			//Idea is to copy one list, then scroll the other, find lowest number
			//and use that, then delete it, then take remaing members and add them
			foreach (T t in thisList) {

				if (mergeList.Contains(t) )
				{
					if (thisList.GetItemCount(t) <= mergeList.GetItemCount(t) )
					{ finalList.AddNode(this.itemBag.GetNode(t));}

					else { finalList.AddNode(ABag.itemBag.GetNode(t)); }
				}
			}
			Console.WriteLine ("Intersection of the lists is:");
			finalList.toString();
		}

		//Display the the contents of the Bag
		public void displayBag ()
		{
			if (itemBag == null) {
				Console.WriteLine ("Empty Bag.");
				return;
			}
			itemBag.toString();
		}
		//Deletes the whole list of Objects
		public void deleteAll ()
		{
			itemBag = null;
		}

		//return the list
		private List<T> getList ()
		{
			return itemBag;
		}
	}
}