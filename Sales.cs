namespace TutorialsEU_MassEmailSender
{
    public class Sales
    {
        Sales()
        {
            var servers = (
                Gmail: (Address: "smtp.gmail.com", Port: 587),
                Yahoo: (Address: " 	smtp.mail.yahoo.com", Port: 587),
                Office365: (Address: "smtp.office365.com", Port: 587),
                AOL: (Address: "smtp.aol.com", Port: 587)
            );
            CurrentSalesRep = new("Jafar", "tutorialseutester@gmail.com", "wroesqeawxjhbcws", servers.Gmail);
        }

        public Sender CurrentSalesRep;
        public static Sales Rep => new();
    }
}
    