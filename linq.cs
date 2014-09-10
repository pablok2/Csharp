//Pavel Gorelov

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2Application
{
    class Program
    {
        static void Main(string[] args)
        {

            //Enumerable collection type List
            List<int> listOfInts = new List<int> { 6,5,1,2,3,4,5,6,7,89,99 };
            
            //Usage of an enumerator
            var enumerator = listOfInts.GetEnumerator();
            while (enumerator.MoveNext())
                Console.Write(enumerator.Current);

            Console.WriteLine();

            //Usage of an iterator
            foreach (int i in listOfInts)
                Console.Write(i);

            Console.WriteLine();

            //Set of lambda queries
            Console.WriteLine("\nLambda query 1:");
            IEnumerable<int> queryGetNums =
                listOfInts
                .Where(n => n >= 5 && n <= 7)
                .OrderByDescending(n => n)
                .Select(n => n);

            foreach (int i in queryGetNums)
                Console.WriteLine(i);

            Console.WriteLine("\nLambda query 2:");
            IEnumerable<int> queryGet6 =    
                queryGetNums
                .Where(n => n == 6)
                .Select(n => n);

            foreach (int i in queryGet6)
                Console.WriteLine(i);

            //set of comprehension queries
            Console.WriteLine("\nComprehension query:");
            string[] compQuery = {"Pablo", "Pavel", "Pasha", "Pabs",
                                 "Pav", "Pavlo", "Chuckles", "Chicken",
                                 "Aboo", "Supernut", "P-Dawg"};
            IEnumerable<string> queryCompQuery =
                from n in compQuery
                where n.Contains("Pa")
                select n;

            foreach (string i in queryCompQuery)
                Console.WriteLine(i);

            Console.WriteLine("\nComprehension query 2:");
            IEnumerable<string> queryCompQuery2 =
                from n in queryCompQuery
                where n.Length > 3
                orderby n
                select n;

            foreach (string i in queryCompQuery2)
                Console.WriteLine(i);

            //deferred execution
            Console.WriteLine("\nDeferred execution:");
            List<string> defExecFail = compQuery.ToList();

            IEnumerable<string> defExecQueryFail = defExecFail
                .Where(n => n.Length > 5)
                .Select(n => n);

            defExecFail.Clear();

            foreach (string i in defExecQueryFail)
                Console.WriteLine(i);

            //deferred execution correction
            Console.WriteLine("\nDeferred execution fixed:");
            List<string> defExec = compQuery.ToList();

            defExec.Add("Latisha");

            List<string> defExecQuery = defExec
                .Where(n => n.Length > 5)
                .Select(n => n)
                .ToList();

            defExec.Clear();
            foreach (string i in defExecQuery)
                Console.WriteLine(i);

            Console.WriteLine("\nDeferred execution:(chaining decorators)");
            IEnumerable<string> chainDecor = new string[] { "plastic", "enjoy", "compose" }
                .Where(n => n.Length > 5)
                .OrderBy(n => n)
                .Select(n => n.ToUpper());

            foreach (string i in chainDecor)
                Console.WriteLine(i);

            //subqueries
            Console.WriteLine("\nSubqueries:");

            IEnumerable<string> subQuery =
                from n in compQuery
                where n.Contains(
                    //query the shortest sequence
                    (from n2 in compQuery orderby n2.Length select n2).First())
                select n;

            foreach (string i in subQuery)
                Console.WriteLine(i);

            //query composition strategies
            Console.WriteLine("\nComposition strategies: (progressive)");
            IEnumerable<string> compStrat01 =
                compQuery.Where(n => n.Contains("a"));
            IEnumerable<string> compStrat02 =
                compStrat01.Select(n => n.ToUpper());

            foreach (string s in compStrat02)
                Console.WriteLine(s);


            //query composition strategies
            Console.WriteLine("\nComposition strategies: (into keyword)");
            IEnumerable<string> compStrat2 =
                from n in compQuery
                where n.Contains("a")
                select n
                    into noA
                    where noA.Length > 3
                    select noA;

            foreach (string s in compStrat2)
                Console.WriteLine(s);

            //projection strategies
            Console.WriteLine("\nProjection strategies: (let keyword)");
            IEnumerable<string> profStrat =
                from n in compQuery
                let x = n.Substring(0, 3)
                select x;

            foreach (string s in profStrat)
                Console.WriteLine(s);

            //projection strategies, anonymous types
            Console.WriteLine("\nProjection strategies: (Anon Types)");
            var anonTypesOrig = from n in compQuery
                                select new
                                {
                                    CutWord = n.Substring(0, 3)
                                };

            IEnumerable<string> anonTypes =
                from k in anonTypesOrig
                select k.CutWord;

            foreach (string s in anonTypes)
                Console.WriteLine(s);

            

            Console.ReadLine();
        }
    }
}
