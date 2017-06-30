using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Xml;

class SAXParsing
{
    public static IEnumerable<XElement> GetBooks()
    {
        XmlReader reader = XmlReader.Create("books.xml");
        reader.MoveToContent();
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "book")
            {
                yield return (XElement)XElement.ReadFrom(reader); //creates a new xml element object
            }
        }
    }
}


class Program
{
    static void Main()
    {
        //LINQ = DOM XML parsing
        // XElement domroot =  XElement.Load("books.xml");  //creates dom tree  //DOM Parsing
        //DOM Parsing //IEnumerable<XElement> booksel = domroot.Descendants("book");

        IEnumerable<XElement> booksel = SAXParsing.GetBooks(); //SAX Parsing //

        IEnumerable<string> query = from bookel in booksel
                                    where bookel.Attribute("author").Value == "A1"
                                    select bookel.Attribute("title").Value;

        IEnumerator<string> et = query.GetEnumerator();
        //query execution

        //Executes the query and obtain the List<> object vontaining titles
        //List<string> titles = query.ToList();
        while (et.MoveNext())
        {
            Console.WriteLine(et.Current);
        }


    }
}
