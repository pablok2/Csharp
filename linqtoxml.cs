//Pavel Gorelov
//Project 3 - C#
//Prof. Suad Alagic

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project3
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * XML specification for the collection from project 2
             * 
             * <nicknames>
             *      <name id = "1">
             *          <firstname>Pablo</firstname>
             *      </name>
             *      .
             *      .
             *      .
             * </nicknames>
            */

            //collection from assignment 2
            string[] compQuery = {"Pablo", "Pavel", "Pasha", "Pabs",
                                 "Pav", "Pavlo", "Chuckles", "Chicken",
                                 "Aboo", "Supernut", "P-Dawg"};

            //xml document wrapper
            XElement collectionXML = new XElement("nicknames");

            //increment for id attribute
            int currentItem = 0;

            //loop to build xdom objects and add them to collectionXML
            foreach (string s in compQuery)
            {
                XElement name =
                    new XElement("name", new XAttribute("id", currentItem),
                        new XElement ("firstname", s)
                );

                collectionXML.Add(name);
                currentItem++;
            }

            //output to local file
            System.IO.File.WriteAllText(@"C:\Users\Public\output.xml", 
                collectionXML.ToString());

            //load up the file
            XElement xmlFile = XElement.Load(@"C:\Users\Public\output.xml");

            //output the XML representation of the object
            Console.WriteLine("output the XML representation of the object");
            Console.WriteLine(xmlFile.ToString());

            //query an xml object
            Console.WriteLine("query an xml object");
            IEnumerable<string> xmlQuery =
                from nicknames in xmlFile.Elements()
                from name in nicknames.Elements()
                where name.Value.Contains("Pa")
                select name.Value;

            //output the query result
            foreach (string s in xmlQuery)
                Console.WriteLine(s);

            //node navigation
            Console.WriteLine("node navigation");

            XNode nodeStep = xmlFile.FirstNode;
            for ( int i = 0; i < xmlFile.Descendants("name").Count(); i++ )
            {
                Console.WriteLine("There goes number: " + i);
                nodeStep = nodeStep.NextNode;
            }

            //return quantity of "name" elements
            Console.WriteLine("There are: " 
                + xmlFile.Elements("name").Count() 
                + " elements in the xml file.");

            Console.WriteLine("Get name for item with attribute id = 7:");
            IEnumerable<string> getID =
                from nicknames in xmlFile.Elements()
                from id in nicknames.Attributes()
                where nicknames.HasAttributes && id.Value == "7"
                select id.Parent.ToString();

            //output the result
            foreach(string s in getID)
                Console.WriteLine(s);

            //remove xdom elements containing "Pa" through parent
            Console.WriteLine("remove xdom elements containing \"Pa\"");
            xmlFile.Elements() //<nicknames>
                .Elements() //<names>
                .Where(n => n.Value.ToString().Contains("Pa"))
                .Remove();
            
            //remove by attribute
            xmlFile.Elements().Where(e => (string) e.Attribute("id") == "0")
                .Remove();

            //output the result
            foreach (XElement element in xmlFile.Elements())
                Console.WriteLine(element.ToString());

            //child node modification
            XElement newElement = new XElement("name");
            newElement.SetAttributeValue("id", "789");
            newElement.SetElementValue("firstname", "Carlos");
            
            //output new xdom object
            Console.WriteLine("output new xdom object: ");
            Console.WriteLine(newElement.ToString());

            //update the main xdom object
            Console.WriteLine("update the main xdom object");
            xmlFile.Add(newElement);
            Console.WriteLine(xmlFile.ToString());

            //retreive XElement by reference and update
            XElement firstDude = xmlFile.Elements().First();
            firstDude.SetElementValue("firstname", "penguin");

            //output the result
            Console.WriteLine(xmlFile.ToString());
            //the "firstDude" object was updated in xmlFile

            Console.ReadLine();
        }
    }
}
