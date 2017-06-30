using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void F1(int data)
    {
        Console.WriteLine(data);
    }
    static void Main(string[] args)
    {
        //Action<int> a1 = new Action<int>(F1) //1
        //Action<int> a1 = new Action<int>(data => Console.WriteLine(data)); //2

        //A1 is reference variable to address delegate ibject that  holds  the address of a function
        // Lamda is converted to a tree data structure

        Action<int> a1= data=> Console.WriteLine(data); //3

        a1(100); // Calls to the function using the delagate object

        //a2 is a referede var. to the expression ibjhect that holds tree data struccture
        //Lamda is converted to tree daata structure
        Expression<Action<int>> a2 = data => Console.WriteLine(data);

        //Traversing a tree data structure
        Console.WriteLine(a2.Parameters[0]);
        Console.WriteLine(a2.Body);

        Action<int> a3 = a2.Compile(); //compile the tree data structure to MSIL
        a3(100);

    }
}
