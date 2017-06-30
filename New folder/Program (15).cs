using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

//Inhereting is done to get the refernce of Main app domian object from another app domian
class Loader : MarshalByRefObject
{
    public void callsum()
    {
        Console.WriteLine("Enter the Dll path");
        string dllPath = Console.ReadLine();

        //Loads the dll
        Assembly ass = Assembly.LoadFile(dllPath);

        //LOads the class by creating the Type object
        Type type = ass.GetType("MyMath");

        //Creating  the object of MyMath class
        object obj = Activator.CreateInstance(type);

        //obtain the reference of sum method
        MethodInfo method = type.GetMethod("sum");

        object result = method.Invoke(obj, new object[] { 10, 20 });
        Console.WriteLine(result);

        //MyMath m = new MyMath();
        //double result = m.sum(1555320, 203333);
        //Console.WriteLine(result);
    }
}


class Program
{
    static void Main()
    {
        AppDomain appdomain1 = AppDomain.CreateDomain("appdomain1");

        //Creating the object in below fashion would create the object in MAin app domian
        //Loader l = new Loader();

        //loading the Loader class in New Domain 1 not in main appp domain is accomplished by below format
        Loader l = appdomain1.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName,"Loader") as Loader;

        //To know it is proxy objet or not
        Console.WriteLine(RemotingServices.IsTransparentProxy(l));

        l.callsum();

        // This will unload loader class, mymathclass and other objects and MathLibray.dll
        AppDomain.Unload(appdomain1); 
        

    }

}