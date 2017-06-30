using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class FileReader
{
    public static  void Read()
    {
        StreamReader sr = new StreamReader("data.txt");
        string str = sr.ReadToEnd();

        Console.WriteLine(str);
        sr.Close();// use using block for such cases
        // using converts to try and finally
        // no exceptions are caught
    }
}

class Program
{
    static void Main()
    {
        FileReader.Read();
        Console.ReadLine();

    }
}

