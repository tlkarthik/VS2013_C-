using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ATLProject1Lib;

//MEF
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Runtime.InteropServices;

interface IManager
{
    void DoWork();
}



[Export(typeof(IManager))]   // EXport is required to create the objects of the class
class FileManager : IManager ,IDisposable    
{
    private StreamReader reader;

    public FileManager()
    {
        reader = new StreamReader("data.txt");
    }   

    public void DoWork()
    {
 	    Console.WriteLine("FileManager : Do work");
    }

    void Close()
    {
        reader.Close(); //Kills unmanaged code
        GC.SuppressFinalize(this);
    }

    ~FileManager()
    {
        Close();
    }

    public void Dispose()
    {
        Close();
    }
}

[Export(typeof(IManager))] 
class StackManager : IManager , IDisposable
{
    private object obj;

    public StackManager()
    {
        obj = new MyStack();
    }

    public void DoWork()
    {
        Console.WriteLine("StackManager : Do work");
    }

    void Release()
    {
        Marshal.FinalReleaseComObject(obj); //Kills the run time callable wrapper object (RCW) Com Object
        GC.SuppressFinalize(this); // If already killed object is found releasing again then this function is used
    }

    ~StackManager()
    {
        Release();
    }

    public virtual void Dispose() 
    {
        Release();
    }
}

class StringManager
{
    private string s;

    public StringManager()
    {
        s = "Hello";
    }
}

/// <summary>
/// ///// Main appp
/// </summary>

class MainApp
{
    [ImportMany]
	public List<IManager> Managers { get; set; }

    private void Compose()
    {
        //discover all class in exports
        // create theri objects
        //Injects into the exe.

        //Threes steps 3 lines of code
        AssemblyCatalog asm = new AssemblyCatalog(Assembly.GetExecutingAssembly());
        CompositionContainer container = new CompositionContainer(asm);

        container.ComposeParts(this);
    }

    public void StartWork()
    {
        Compose();

        if (Managers.Count !=0)
	    {
            foreach (IManager manager in Managers)
	        {
                if (manager is IDisposable)
                {
                     manager.DoWork();
                     (manager as IDisposable).Dispose();
                }
	        }
	    }
    }

}

class Program
{
    static void Main()
    {
        MainApp app = new MainApp();
        app.StartWork();
        Console.ReadLine();

    }
}


