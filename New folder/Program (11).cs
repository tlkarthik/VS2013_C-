using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//any name we can give
class WindowsAPI
{
    //static is used as message box is c function and c do not have 'this' key word
    //extern beyond the .net
    [DllImport("user32.dll")] // it is in system32 folder

    public static extern void MessageBox(int n, string body, string caption, int m);
    //CLR HAndle h = internally gets the handke of the dll or library
    //CLR: FUNPTR f will get by calling GetProcAddress(h, "MessageBox");
    //CLR : f(0,"body","caption",0)



}

struct Data
{
    public int x;
    public int y;
} // Matches struct in c

[StructLayout(LayoutKind.Explicit ) ]
struct Value
{
    [FieldOffset(0)] public int y;
    [FieldOffset(0)] public int x;
}

delegate void Result (int x); //MAtches function pointer in c

class OurCAPI
{
    // put the MyWindowsLibrary.dll in to the debug folder of App16
        [DllImport("MyWindowsLibrary.dll")]
        public static extern double sum(double x, double y);

        [DllImport("MyWindowsLibrary.dll")]
        public static extern double mul(Data d);

        [DllImport("MyWindowsLibrary.dll")]
        public static extern double sub(Value v);

        [DllImport("MyWindowsLibrary.dll")]
        public static extern void div(int x, int y, ref int result);

        [DllImport("MyWindowsLibrary.dll")]
        public static extern void sqr(int x, out Result result);  /* function pointer is equivakent to delegate in .NET*/ 

}

class Program
{
    static void Main(string[] args)
    {
        //WindowsAPI.MessageBox(0, "Body", "caption", 0);
        Console.WriteLine("Sum :  " + OurCAPI.sum(10,20));

        Data d; d.x = 10; d.y = 20;
        Console.WriteLine("Mul :  " + OurCAPI.mul(d)); //passing the value

        Value v; v.x = 10;v.y = 20;
        Console.WriteLine("Sub :  " + OurCAPI.sub(v));  //Passing the value V

        int result = 0;
        OurCAPI.div(20,10, ref result);
        Console.WriteLine("Div :  " +result);

        //OurCAPI.sqr(10, res =>{(Console.WriteLine("SQR : " + res);} );

    }
}

