using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


class Work
{
    int[] items = new int[10];
    int count = 1;

    //events are used for signalling across threads
    public AutoResetEvent ae = new AutoResetEvent(false);  //fasle => non signal state
    public ManualResetEvent me = new ManualResetEvent(false); //fasle => non signal state
    private ReaderWriterLock rw = new ReaderWriterLock(); 

    public void Fill()
    {
        while (true)
        {
            // wait for MT's signal
            ae.WaitOne(); // calling thread goes to wait state if the state is false
            //means the thread will not get the quantum from OS (efficient wait state)
            //When event objects state become true the thread comes out of the wait state 
            //this event objects state automatically resets to false after the thread
            //comes out of wait state - Hence Auto reset event

            rw.AcquireWriterLock(Timeout.Infinite);
            for (int i = 0; i < 10; i++)
            {
                items[i] = count;
            }
            count++;
            rw.ReleaseWriterLock();
            me.Set();  //signalling multiple threads
        }
    }

    public void Display()
    {
        while (true)
        {
            //wait for fills signal
            me.WaitOne();   //doesnt automatically reset to false
            me.Reset(); //MAnual reset

            rw.AcquireReaderLock(Timeout.Infinite);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(items[i]);
            }
            rw.ReleaseReaderLock();
        }
    }
    
    public void Save()
    {
        while (true)
        {
            //wait for fills signal
            me.WaitOne(); //doesnt automatically reset to false
            me.Reset(); //manual reset

            rw.AcquireReaderLock(Timeout.Infinite);

            StreamWriter writer = new StreamWriter("data.txt");
            for (int i = 0; i < 10; i++)
            {
                writer.WriteLine(items[i]);
            }

            rw.ReleaseReaderLock();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Work w = new Work();
        Task.Run(() => { w.Fill(); }); //st1
        Task.Run(() => { w.Display(); }); //st2
        Task.Run(() => { w.Save(); }); //st3
        
         while (true)
        {
            Console.ReadLine();
            //wait for fills signal
        }

    }
}
