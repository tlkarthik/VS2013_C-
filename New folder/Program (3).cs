using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Disk
{
    public static int count;

    public static void Traverse(string folder)
    {
        try 
	    {
            string[] files = Directory.GetFiles(folder, "*.*");
            count = count + (files.Length);
        }
         catch (Exception)
	    {

            return;
	    }  

        string[] subdirectories = Directory.GetDirectories(folder);

        //foreach (string subdirectory in subdirectories)
        //{
        //    Traverse(subdirectory);
        //}
        //Paralel processing
        Parallel.ForEach(subdirectories, subdirectory =>
        {
            Traverse(subdirectory);
        });
    }

}

class Program
{
    static void Main(string[] args)
    {
        Stopwatch sw = Stopwatch.StartNew();
        Disk.Traverse(@"C:\Windows");
        Console.WriteLine(sw.ElapsedMilliseconds);
        Console.WriteLine(Disk.count);
    }
}
