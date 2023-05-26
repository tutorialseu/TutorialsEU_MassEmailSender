namespace TutorialsEU_MassEmailSender
{
    public class Sender
    {
        public Sender(string displayName, string address, string password, (string Address, int Port) server)
        {
            MailServer = server;
            Credentials = (address, password);
            Contact = new(displayName, address);
        }
        public (string Address, int Port) MailServer;
        public (string UserName, string Password) Credentials;
        public Contact Contact;
    }
}
