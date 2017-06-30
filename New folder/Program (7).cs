
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Counter
{
    private int count;
    private object sync = new object();
    public void Increment()
    {
        count++;
    }
}

//This kind of concurrent scync in libraries are created only when more than 2 threads are launched
//If only one thread is lainched this approach is waste because it slows the performance unneccasrily
// This is called Thread safe class
class CounterConcurrent
{
    private int count;
    private object sync = new object();
    public void Increment()
    {
        Monitor.Enter(sync);  // For  creating libraries for such kind of Async issues
        count++;
        Monitor.Exit(sync); // For  creating libraries for such kind of Async issues
    }
}
 
// This class is equivalent to above class
// This class can be best used for GetFilesCount in the directory app which discussed earlier
class EquivalentCounterCount
{
    private int count;
    private object sync = new object();
    public void Increment()
    {
        Interlocked.Increment(ref count);
        count++;
        //Interlocked.Add(ref count, 2); //This is not required.. Try figuring out this significance
    }
}

class Program
{
    static void Main()
    {
        //MT
        Counter c = new Counter(); // c is refernce type object. Which has 2 extra columns . they
                                    // are sync block and object pointer

        // Task uses thread pool thread (Back ground thread)
        //Main thread exits main, then all back ground thread would be kiilled
        // But in fore groound thread, Main thread waits till all threads get executed
        // Fore goroudn is instatiated using Thread class 
       
        Task t1 = Task.Run(() =>   //ST1
        {
            Monitor.Enter(c);
            c.Increment();
            Console.WriteLine("ST1");
            Monitor.Exit(c);
        });

        Task t2 = Task.Run(() =>  //ST2
        {
            Monitor.Enter(c);
            c.Increment();
            Console.WriteLine("ST2");
            Monitor.Exit(c);
        });

        Task.WaitAll(t1, t2); // Waits for all ST1 ,ST2 ends

        Console.WriteLine("Main thread start and end"); //MT started
        //Console.ReadLine();
    }
}
