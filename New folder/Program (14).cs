using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

static class MyExtensions
{
    public static void Display<T>(this ListView lv, IEnumerable<T> collection)
    {
        if (lv.View != View.Details)
            throw new Exception("Its not display view");


        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            lv.Columns.Add(property.Name);
        }

        Task.Run(() =>
            {
                foreach (T item in collection) //for each object in collection
                {
                    //Thread.Sleep(3000);
                    lv.Invoke(new Action
                        (() =>
                            {
                                PropertyInfo[] properties = typeof(T).GetProperties();  //get the values of all its properties
                                ListViewItem lvitem = lv.Items.Add(properties[0].GetValue(item).ToString());

                                for (int i = 1; i < properties.Length; i++)
                                {
                                    lvitem.SubItems.Add(properties[i].GetValue(item).ToString());
                                }
                            }
                        ));
                }
            });
    }
}

class Book
{
    public string title { get; set; }
    public double price { get; set; }
}

class Myform : Form
{
    ListView lv;

    public Myform()
    {
        lv = new ListView();
        this.Size = new Size(450, 500);
        this.Controls.Add(lv);
        lv.Size = new Size(300, 300);

        List<Book> books = new List<Book>()
        {
            new Book {title = "T1",price = 1000},
            new Book {title = "T2",price = 3000},
            new Book {title = "T3",price = 2000}
        };

        lv.View = View.Details;
        lv.Display(books);
    }
}

class Program
{
    static void Main()
    {
        Myform f = new Myform();
        f.ShowDialog();
    }
}

