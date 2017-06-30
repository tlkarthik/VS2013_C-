using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }


    //Converts the function to state machine class. (Internally uses yeild)
    private async void button1_Click(object sender, EventArgs e)
    {
        BookRepository rep = new BookRepository();

        //await sends the MT or task object to the message loop only if the task object is found incomplete

        //As soon as the task is marked complete the MT will  be brought back
        //From the message loop and assignement will get executed
        List<Book> books = await rep.GetBooksTaskAsync(); //MT

        //display the information in list view
        Task.Run(() =>
            {
                foreach (Book item in books)
                {
                    PropertyInfo[] properties = typeof(Book).GetProperties();  //get the values of all its properties
                    ListViewItem lvitem = listView1.Items.Add(properties[0].GetValue(item).ToString());

                    for (int i = 1; i < properties.Length; i++)
                    {
                        lvitem.SubItems.Add(properties[i].GetValue(item).ToString());
                    }
                }
            });
    }
}

class Book
{
    //represent a column
    public int BookID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public double Price { get; set; }
}

class BookRepository
{
    public Task<List<Book>> GetBooksTaskAsync()
    {
        string connectionstring = @"server =.\sqlexpress;database = intdb;integrated security = true";
        SqlConnection connection = new SqlConnection(connectionstring);

        //prepare query
        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "Select * from books"; //better use skip and take


        TaskCompletionSource<List<Book>> tcs = new TaskCompletionSource<List<Book>>();
        connection.Open();
        //gives the job of obtainign the cursor toIO completion port and
        // releae the main thread


        command.BeginExecuteReader(result =>
        {
            //IO completeion port will call this function after obtaining the cursor in ST. 
            // This iss required to read cursor and tell the ST that results are reached
            SqlDataReader reader = command.EndExecuteReader(result);
            List<Book> books = new List<Book>();

            while (reader.Read()) // in ST obtaining the 1 record at a time from Db
            {
                books.Add(new Book()
                {
                    BookID = (int)reader["BookId"],
                    Title = (string)reader["Title"],
                    Author = (string)reader["Author"],
                    Price = (double)reader["Price"]
                });
            }
            connection.Close();
            //MArk the task object as complete and send result to MT
            tcs.SetResult(books);

        }, null
            );  //registerin a call back with IO completeion port. 2parametr should be null
        return tcs.Task; //Incomplete task (MT)
    }
}

