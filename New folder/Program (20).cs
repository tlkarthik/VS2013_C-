using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//For MEF
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;



class UI
{
    [ImportMany]
    public List<IMessageType> MessageTYpes { get; set; }
    
    [ImportMany]
    public List<IMessageSender> MessageSenders { get; set; }

    private void Compose()
    {
        //Run MEF
        DirectoryCatalog dir = new DirectoryCatalog("parts"); // Parts must me there in which .exe is there

        CompositionContainer container = new CompositionContainer(dir); //Goes to Parts folder and loads all the dll's

        container.ComposeParts(this);
    }

    public void start  ()
    {
        Compose();

        if(MessageTYpes.Count ==0 || MessageSenders.Count ==0)
        {
            Console.WriteLine("Nothing is found");
            return;
        }


        int index = 1;
        foreach (IMessageType mt in MessageTYpes)
        {
            Console.WriteLine(index + " : " + mt.GetName());
            index++;
        }

        Console.Write("Select the type: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        string message = MessageTYpes[choice - 1].GetMessage();

        index = 1;

        foreach (IMessageSender sender in MessageSenders)
        {
            Console.WriteLine(index + " :  " + sender.GetName());
            index++;
        }

        Console.Write("Select the sender: ");
        choice = Convert.ToInt32(Console.ReadLine());

        MessageTYpes[choice - 1].GetMessage();
    }
}


class Program
{
    static void Main(string[] args)
    {
        UI ui = new UI();
        ui.start();

        Console.ReadLine();
    }
}
