using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns2
{
    public class NotificationService
    {
        public String Client { get; set; }

        INotification emailSender { get; set; }
        
        public void SendEmail()
        {
            emailSender = new SMTPEmailSender();
            emailSender.SendEmail();
        }
    }

    public interface INotification
    {
        void SendEmail() { }
    }

    public class SMTPEmailSender:INotification
    {
        public void SendEmail()
        {
            Console.WriteLine("Отправить Email");
        }
    }
}
