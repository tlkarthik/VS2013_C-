using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//WPF
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

//WPF pgm

class MyWindow : Window
{
    Button LoadImagesBtn;
    ListBox ImagesList;

    public MyWindow()
    {
        LoadImagesBtn = new Button();
        LoadImagesBtn.Content = "Load Images";

        ImagesList = new ListBox();
        ImagesList.Height = 400;
        ImagesList.Background = Brushes.Yellow;

        //Creating panel and adding to it
        StackPanel panel = new StackPanel();    
        panel.Children.Add(LoadImagesBtn);
        panel.Children.Add(ImagesList);

        this.Content = panel; //Child of window

        //Registering event handler
        LoadImagesBtn.Click += LoadImagesBtn_Click;
    }

    void LoadImagesBtn_Click(object sender, RoutedEventArgs e) //MT enters
    {
        //MT enters a another message loop
        System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

        //MT enters a new  message loop such that ignores the message of parent window
        if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            //Load images and display them 
           string[] files =  Directory.GetFiles(@"E:\karthik\Photos\phone_pics","*.jpg"); //MT

            //Never ever call GUI components from 2 thread.
            //Locking is not provided for GUI components as it even slows in Main thread

            //calling main thread
           System.Windows.Threading.Dispatcher mt = System.Windows.Threading.Dispatcher.CurrentDispatcher;

            //It needs to be ran in secondary loop
           Task.Run(() =>
               {
                   foreach (string file in files)
                   {
                       //calling lambda data in memorey //MT
                       mt.Invoke(() =>
                           {
                               //Load imagedata in memorey
                               BitmapImage bi = new BitmapImage();
                               bi.BeginInit();
                               bi.DecodePixelHeight = bi.DecodePixelWidth = 100;
                               bi.UriSource = new Uri(file, UriKind.Absolute);
                               bi.EndInit();

                               //Add to UI control
                               Image img = new Image();
                               img.Height = img.Width = 100;
                               img.Source = bi;

                               ImagesList.Items.Add(img);  //MT
                           }, System.Windows.Threading.DispatcherPriority.SystemIdle);
                   }
               });
        }
    }
}

class Program
{
    //CLR calls MAin using Main thread
    [STAThread] //Making main thread enter STA
    static void Main() //MT
    {
        //WPF
        MyWindow w = new MyWindow(); //MT
        w.ShowDialog();  //Showdialog() starts the message loop. where as show() do not start message loop
    }
}

