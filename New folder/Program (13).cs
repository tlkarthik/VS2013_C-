using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Net;

class MyForm : Form
{
    TextBox url = new TextBox();
    Button getHTML = new Button();
    TextBox displayHTML = new TextBox();

    public MyForm()
    {
        this.Controls.Add(url);
        this.Controls.Add(getHTML);
        this.Controls.Add(displayHTML);
        getHTML.Location = new System.Drawing.Point(100, 100);
        displayHTML.Location = new Point(0, 50);

        url.Location = new Point(0, 150);
        url.Multiline = true;
        getHTML.Text = "getHTML";
        //this.Size =new Size

        getHTML.Click += getHTML_Click;           
    }

    //this function will now bre tronsformed to state machine class
   async void getHTML_Click(object sender, EventArgs e) //MT
    {
        WebClient client = new WebClient(); //MT
       //Here MT will be released after the requesrt is sent using IO completeion port
       //MT will be released befor the result is available
       //Await sends main thread to message loop
        displayHTML.Text = await client.DownloadStringTaskAsync(url.Text);

    }
}

class Program
{
    static void Main()
    {
        MyForm f = new MyForm();
        f.ShowDialog();
    }
}
