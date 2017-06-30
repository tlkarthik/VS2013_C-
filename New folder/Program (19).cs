using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class utility
{
    public static bool IsEven(this int value)
    {
        return value % 2 == 0;
    }

    public static int MySum(this IEnumerable<int> items)
    {
        int sum = 0;

        IEnumerator<int> et = items.GetEnumerator();
        while (et.MoveNext())
        {
            sum += et.Current;
        }

        //ABove code is equivalent to for each loop
        
        //for (int i = 0; i < items.Length; i++)
        //{
        //    sum += items[i];
        //}
        return sum;
    }

    //All collection classes are encapsulated using IEnumarable like array,list, dictionary ects  collection
}

class MyCollection : IEnumerable<int>
{
    private int[] arr = new int[] { 1, 2, 1, 2, 5 };


    public IEnumerator<int> GetEnumerator()
    {
        foreach (var item in arr)
        {
            yield return item;
        }
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}


class Program
{

    ////static int GetData()
    ////{
    ////    return 10;
    ////}

    static IEnumerable<int> GetData()
    {
/*sate  0*/     yield return 10;        //Get data becomes function and 'state machine' class,. Function now creates object of the class.
                                //Every yeild become part of move next method
/*sate  1*/     yield return 20;        //How many yeilds u have they become part of movenext . so only object of class
/*sate  2*/     yield return 30;        // 
    

        //above code is same as below

        int[] num = new int[] { 10, 20, 30, 45, 50 };
        foreach (int  item in num)
        {
            yield return item;
        }

        //GetData from main pgm call move next on Getdata and Getdata internall calls move next on array

    }

    static void Main(string[] args)
    {
        //int result = GetData();
       // Console.WriteLine(result);

        foreach (var item in GetData()) // Here returns object of GetData class which is implementing Ienumerable which has 
                                        // MOveNext and current methods which returns 10
        {
            Console.WriteLine(item);
        }

        ////int i = 15;
        ////Console.WriteLine(i.IsEven());  // i.ISEven() extension method 'this' is required at function

        ////int[] numbers = new int[] { 10, 20, 30, 50, 60 };
        ////Console.WriteLine(utility.MySum(numbers));
        ////Console.WriteLine(numbers.MySum());


        ////MyCollection enumerables = new MyCollection();
        ////Console.WriteLine(enumerables.MySum());
    }
}
