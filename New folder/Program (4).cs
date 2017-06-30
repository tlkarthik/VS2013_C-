using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

class Disk
{
    public event Action<string> FolderEntered;

    public void Traverse(string folder, CancellationTokenSource cts)
    {
        if(cts.IsCancellationRequested)
            return;

        if (FolderEntered != null)
            FolderEntered(folder);

        try
        {
            string [] files = Directory.GetFiles(folder,"*.*");
        }
        catch (Exception)
        {
            
            throw;
        }
        
        
        string[] subDirectories = Directory.GetDirectories(folder);

        foreach (String  subDirectory in subDirectories)
	    {
           
            Traverse(subDirectory,cts);
	    }
    }
}

class MyForm : Form
{
    Label l1;
    public MyForm ()
	{
        l1 = new Label();
        l1.Location = new System.Drawing.Point(0, 0);
        l1.Size = new System.Drawing.Size(25, 25);


        Button b = new Button();
        this.Controls.Add(b);

        b.Location = new System.Drawing.Point(50, 100);
        b.Text = "DO Work";

        b.Click += b_Click;

        Button stop = new Button();
        this.Controls.Add(stop);
        stop.Text = "Stop";
        stop.Location = new System.Drawing.Point(150, 100);
        stop.Click += stop_Click;

        
	}


    void stop_Click(object sender, EventArgs e)
    {
        cts.Cancel();
    }

    //Task t;
    // This is used for long running compute bound operation
    CancellationTokenSource cts;
    void b_Click(object sender, EventArgs e)
    {
        Disk d = new Disk();
        d.FolderEntered += d_FolderEntered; //2nd thread
        cts = new CancellationTokenSource();

        Task.Run(() =>
            {
                //Called in the sec thread
                d.Traverse(@"C:\",cts);
            },cts.Token);
    }

    void d_FolderEntered(string folder)
    {
         //2nd thread will call

        //Buttom allways update UI with main thread
        //Invoke actuall update the UI 
        this.Invoke(new Action(
            () =>
            {
                // called in MAin thread
                // UPdate the UI here
                l1.Text = folder;
            }
            
            ));

     }
    
}

class Program
{
    static void Main(string[] args)
    {
        MyForm f = new MyForm();
        f.ShowDialog();
    }
}
