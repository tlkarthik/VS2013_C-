
using System;
using System.Collections.Generic;
using System.Linq;

//static class MyExtensions
//{
//    Where will becoem state machine class: Movenext and current
//    public static IEnumerable<T> Where<T>(this IEnumerable<T> collection, Func<T, bool> condition)
//    {
//        foreach (T item in collection)
//        {
//            if (condition(item)) // calling the function that has a condition and passing one number
//                yield return item;
//        }
//    }

//    Select also is coneverted to stae machine class: Move next and current
//    public static IEnumerable<TReturn> Select<T, TReturn>(this IEnumerable<T> collection, Func<T, TReturn> projection)
//    {
//        foreach (T item in collection)
//        {
//            yield return projection(item); //Calling the function that has code
//        }
//    }
//}

class Book
{
    public int BookID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
}

class Program
{


    static void Main()
    {
        List<Book> books = new List<Book>();
        books.Add(new Book() { BookID = 1, Title = "T1", Author = "A1", Price = 1000 });
        books.Add(new Book() { BookID = 2, Title = "T2", Author = "A2", Price = 2000 });
        books.Add(new Book() { BookID = 3, Title = "T3", Author = "A1", Price = 3000 });
        books.Add(new Book() { BookID = 4, Title = "T4", Author = "A1", Price = 4000 });


        IEnumerable<string> query = (from book in books
                                    where book.Author == "A1"
                                    select book.Title).Skip(2).Take(1) ;

        IEnumerator<string> et = query.GetEnumerator();
        //Query eexcution
        while (et.MoveNext())
        {
          //  Console.WriteLine(et.Current);
        }

        IEnumerable<double> Querysum = (from book in books
                                     where book.Author == "A1"
                                     select book.Price);

        Console.WriteLine(Querysum.Sum());
        Console.WriteLine(Querysum.Select(Price => Price-1).Sum());
        Console.WriteLine(Querysum.Select(Price => Price - 100).Sum());
        Console.WriteLine(Querysum.Where(Price => Price > 1000).Sum());
        }
    }


    //static void Main(string[] args)
    //{
    //    List<int> numbers =  new List<int>();
    //    numbers.Add(10);
    //    numbers.Add(20);
    //    numbers.Add(30);
    //    numbers.Add(40);

    //    //LINQ query preparation
    //    var query = from number in numbers
    //                where number > 10 && number < 50
    //                select number;

    //    //Execute query
    //    //foreach (var item in query)
    //    //{
    //    //    Console.WriteLine(item);
    //    //}

    //    //Equivalent to below code -- for each expansion

    //    IEnumerable<int> en = query;
    //    IEnumerator<string> et = query.GetEnumerator();

    //    while (et.MoveNext())
    //    {
    //        Console.WriteLine(et.Current);
    //    }
    //}
//}

