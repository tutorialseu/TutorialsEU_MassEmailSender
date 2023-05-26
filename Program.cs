using System.Net;
using System.Net.Mail;
using TutorialsEU_MassEmailSender;
using System.Text.Json;
public class Program
{
    public static void Main(string[] args)
    {
        //Set the console to white and text to black
        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Black;

        Console.WriteLine("Welcome to the TutorialsEU email marketing service.");
        Console.WriteLine($"You are currently logged in as {Sales.Rep.CurrentSalesRep.Contact.Name}");
        Console.WriteLine($"Make sure to include a 'customers.json' file in the '{Directory.GetCurrentDirectory()}' directory.");
        Console.WriteLine("Press any key to start the process");
        Console.ReadKey();
        PrepEmail();
    }

    public static void PrepEmail()
    {
        Console.Clear();
        Console.WriteLine("\r Please write an email subject:");
        string emailSubject = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("\r Please write an email body:");
        string emailBody = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("\rThe following email will be sent:");
        Console.WriteLine($"Email Subject: {emailSubject}");
        Console.WriteLine($"Email Body: {emailBody}");

        Console.WriteLine("\rConfirm send? (Y) to send (N) to redo");
        bool sendOrRedo = YNQuestion();
        if (!sendOrRedo) PrepEmail();;

        SendEmail(
            from: Sales.Rep.CurrentSalesRep
            , toAll: Customers()
            , subject: emailSubject
            , body: emailBody
        );

        Console.WriteLine("\rEmail has been sent succesfully! Press any key to send another email or Esc to exit the application");
        ConsoleKey nextStep = Console.ReadKey().Key;
        if (nextStep == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }
        else
        {
            PrepEmail();
        }
    }

    public static bool YNQuestion()
    {
        string userInput;
        do
        {
            userInput = Console.ReadLine() ?? string.Empty;
            userInput = userInput.ToUpper();
        }
        while (userInput != "Y" && userInput != "N");
        return userInput == "Y";
    }

    public static void SendEmail(Sender from, Contact[] toAll, string subject, string body)
    {
        MailMessage message = new()
        {
            From = new MailAddress(from.Contact.EmailAddress, from.Contact.Name),
            Subject = subject,
            Body = body
        };

        foreach (var contact in toAll)
            message.Bcc.Add(new MailAddress(contact.EmailAddress, contact.Name));

        var (Address, Port) = from.MailServer;
        SmtpClient smtpClient = new(Address, Port)
        {
            EnableSsl = true,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(from.Credentials.UserName, from.Credentials.Password)
        };

        smtpClient.Send(message);
    }

    public static Contact[] Customers()
    {
        string customerList = File.ReadAllText("customers.json");
        Contact[] customer = JsonSerializer.Deserialize<Contact[]>(customerList)!;
        return customer;
    }


}