using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//exceptions to throw from class library
[Serializable]
sealed class StackFullException : Exception
{
    public    StackFullException(string message) : base (message)
    {

    }
}

[Serializable]
sealed class StackEmptyException : Exception
{
    public StackEmptyException(string message) : base (message)
    {

    }
}

//exceptions handling
class MyStack
{
    int[] items = new int[10];
    int top = 0;
}
class Program
{
    static void Main(string[] args)
    {
        //collects the unhandeled applications
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
    }

    static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        throw new NotImplementedException();
    }
}
