using System;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;

namespace ClassLibrary_Feedback
{
    class Feedback
    {
        
        // Fedback states
        
        private bool IsSent;

        // Getting parameters

        private string UserName;
        private string Email;
        private string Text;
        private int Raiting;
        private DateTime Date;

        // Checking Name lenght (no more then 20 char)
        public void SetUserName(string UserName)
        {
            _ = UserName != null && UserName.Length <= 20;
        }
        // Checking Email validation
        public bool SetEmailValid(string Email)
        {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(Email);
                    return addr.Address == Email;
                }
                catch
                {
                    return false;
                }
        }
        // Setting Raiting range between 1 and 5
        public void SetRaitingRule(int Raiting)
        {
            _ = Raiting >= 1 && Raiting <= 5;
        }



        // sending the feedback

        public void Send()
        {
            MailAddress from = new MailAddress("UltimateQuarry@gmail.com");
            MailAddress to = new MailAddress("UQ_SupporTeam@gmail.com");
            MailMessage msg = new MailMessage(from, to);
            msg.Body = "From: "+ UserName + ", Contact Email:" + Email + ". Feedback" + Text + ", Service rating: " + Raiting + ", Date: " + Date;

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp@gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, "UQtest0!")
            };
        }
    }
}
