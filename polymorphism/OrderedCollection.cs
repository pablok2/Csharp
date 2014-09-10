using System;

namespace Program1
{
	public interface OrderedCollection<T> : Collection<T> where T : IComparable
	{
		//This function will order the bag
		bool isOrdered ();
	}
}
