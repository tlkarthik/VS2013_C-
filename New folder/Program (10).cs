
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//IID F16AAA5C-B743-4B22-9D68-BBEF1DC5B11C
//[InterfaceType(ComInterfaceType.InterfaceIsDual)] //dual interface
//[Guid("F16AAA5C-B743-4B22-9D68-BBEF1DC5B11C")]  //IID
//interface IMyStack1
//{
//    void Push(int item);
//    void Pop(out int item);
//}

//Above code can be commented if idl -> tlb is added as reference
//And use its name space

using ATLProject1Lib;

class Program
{
    static void Main()
    {
        //Clsid 2603FBAE-6AF1-42B5-B787-DDDB023903CF
        //Goes to the registry and loads COM dll
        //Type type = Type.GetTypeFromCLSID(new Guid("2603FBAE-6AF1-42B5-B787-DDDB023903CF"));

        //this creates com object by calling DLLGetClassObject in the DLL
        //object obj = Activator.CreateInstance(type);

        object obj = new MyStack();

        //Querying for interface => obtaing the address of COM object where address of array of function assress is stored
        IMyStack1 s1 = obj  as IMyStack1;

        if (s1 != null)
        {
            s1.Push(100);
            s1.Push(200);
            s1.Push(300);

            int result;
            s1.Pop(ref result);
        }
        else
            Console.WriteLine("Interface not found");

        //Release com object
        Marshal.FinalReleaseComObject(obj);
    }
}

