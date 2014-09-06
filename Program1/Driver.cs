using System;
using System.IO;

namespace Program1
{
	public class Driver
	{
		static Bag<string> strList = new Bag<string>();
		static Bag<string> strList1 = new Bag<string>();

		static OrderedBag<string> ordList1 = new OrderedBag<string>();
		static OrderedBag<string> ordList2 = new OrderedBag<string>();

		//static myOrderedBag<string> ordBag1 = new myOrderedBag<string>();
		//static myOrderedBag<string> ordBag2 = new myOrderedBag<string>();

		static void Main()
		{
			string fileName1 = ("/home/geo/Desktop/CSharp Class/SorceProject1/Test1/Test1/DataFile1.txt");
			string fileName2 = ("/home/geo/Desktop/CSharp Class/SorceProject1/Test1/Test1/DataFile2.txt");	

			LoadBag(strList, fileName1);
			LoadBag(strList1, fileName2);

			LoadOrdBag(ordList1, fileName1);
			LoadOrdBag(ordList2, fileName2);			

			Console.WriteLine ("Displaying Bags.");
			strList.displayBag();
			strList1.displayBag();

			Console.WriteLine ("Displaying Union Bags.");
			strList.Union(strList1);
			Console.WriteLine ("Displaying Intersection Bag.");
			strList.Intersection(strList1);

			Console.WriteLine ("Displaying deleting from Bag.");
			strList.deleteAll();
			strList.displayBag();
			Console.WriteLine();


			Console.WriteLine ("Displaying Ordered Bag1.");
			ordList1.displayBag();
			Console.WriteLine();

			Console.WriteLine ("Displaying Ordered Bag2.");
			ordList2.displayBag();

			Console.WriteLine ("Displaying Union Ordered Bags.");
			ordList1.Union(ordList2);

			Console.WriteLine ("Displaying Union Ordered Bags.");
			ordList1.Intersection(ordList2);
		}
	
		//This will fill the passed bag with a file full of data for testing
		public static void LoadBag (Bag<string> bag, string fileName)
		{
			FileStream file = new FileStream (fileName, FileMode.Open);
			StreamReader input = new StreamReader (file);

			string line = "";

			while (!input.EndOfStream) {
				try {
					line = input.ReadLine ();
				} catch (Exception e) {
					Console.Write ("No data." + e);
				}
				bag.insert (line);
			}
			file.Close ();
		}

		//Loads the ordered Bag
		public static void LoadOrdBag (OrderedBag<string> OrdBag, string fileName)
		{
			FileStream file = new FileStream (fileName, FileMode.Open);
			StreamReader input = new StreamReader (file);

			string line = "";

			while (!input.EndOfStream) {
				try {
					line = input.ReadLine ();
				} catch (Exception e) {
					Console.Write ("No data." + e);
				}
				OrdBag.insert (line);
			}
			file.Close ();
		}
	}
}