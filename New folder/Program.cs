
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

class Book
{

}
class BooksRepository
{
    public Task<List<Book>> GetBooks()
    {
        //Creates the task object in incompltete state
        TaskCompletionSource<List<Book>> tcs = new TaskCompletionSource<List<Book>>();

        SqlConnection connection = new SqlConnection();

        SqlCommand command = new SqlCommand();
        command.Connection = connection;
        command.CommandText = "select * fromn books"; //Correct query with skip and take query

//        try
  //      {
            List<Book> books = new List<Book>();
            connection.Open();

            command.BeginExecuteReader(result =>
                {
                    //called by IO completion port once cursor is available
                    // ussing secondary thread (IO thread)
                    SqlDataReader reader = command.EndExecuteReader(result);
                    while (reader.Read()) // 1 record at a time
                    {
                        ////Reader["bookID"]
                        ////Reader["Title"]
                        ////Reader["Author"]
                        ////Reader["Publisher"]
                        ////Reader["Price"]
                        ////Book b=new Book() {:::::}
                        ////books.Add( b );
                    }

                    connection.Close();
                    tcs.SetResult(books);

                }, null); //Sends query to Db

            return tcs.Task;
        
        //Execute reader blocks main thread.
        //    while (reader.Read()) // 1 record at a time
        //    {
        //        //Reader["bookID"]
        //        //Reader["Title"]
        //        //Reader["Author"]
        //        //Reader["Publisher"]
        //        //Reader["Price"]
        //        //Book b=new Book() {:::::}
        //        //books.Add( b );
        //    }
        //    return books; //retrun the ref. of List <Book> obj

        //}
        //finally
        //{
        //    connection.Close();
        //}


    }
}

class MyForm : Form // is - a relation (inheriting is a relation)
{
    public MyForm()
    {
        Button b = new Button();
        this.Controls.Add(b);

        b.Location = new System.Drawing.Point(100, 100);
        b.Text = "DO Work";
        b.Click += b_click();
    }

    private EventHandler b_click()
    {
        throw new NotImplementedException();
    }

    async void b_click(object sender, EventArgs e)
    {
        BooksRepository rep = new BooksRepository();
        List<Book> books = await rep.GetBooks(); // await sends message to mesage loop about incompleete task object
    }
}

class Program
{
    //CLR calls main() using Main thread
    static void Main()
    {
        Form f = new MyForm(); //MT
        f.ShowDialog(); //MT

        // 1. shows the window
        // 2. starts the message loop
    }
}


