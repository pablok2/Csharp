using System;

namespace Program1
{
	public interface Collection<T>
	{
		//Inserts one item into the Collection
		bool insert 	(T item);

		//Deletes one item from the Collection
		bool delete 	(T item);

		//Test to see if there is at least one identical member in the Collection
		bool Contains 	(T item);

		//Counts all the items in the collection that are equal to it
		int countItem	(T item);

		//Deletes the whole List
		void deleteAll();
	}
}

