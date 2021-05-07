using System;
using System.Net;
using System.Net.Mail;

namespace ClassLibrary_Feedback
{
    class Feedback
    {
        // Declair parameters

        private string UserName;
        public string username
        {
            get => UserName;
            set => _ =UserName == null || UserName.Length > 20 ? throw new Exception("too long or empty name") :    
            UserName = value;
        }

        private string Email;
        public string email
        {
            get => Email;


            set
            {
#pragma warning disable CS8321 // Local function is declared but never used
                bool IsEmailValid(string Email)
#pragma warning restore CS8321 // Local function is declared but never used
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
                Email = value;
            }
        }



        private string Text;
        public string text
        {
            get => Text;
            set => Text = value;
        }

        private int Raiting;

        public int raiting
        {
            get => Raiting;
            set => _ = Raiting < 1 || Raiting > 5 ? throw new Exception("wrong value") :
                Raiting = value;
        }

        private DateTime Date;

        public DateTime date
        {
            get => Date;
            set => Date = value;
        }
        // sending the feedback

        public void Send()
        {
            MailAddress from = new MailAddress("UltimateQuarry@gmail.com");
            MailAddress to = new MailAddress("UQ_SupporTeam@gmail.com");
            MailMessage msg = new MailMessage(from, to);
            msg.Body = "From: " + username + ", Contact Email:" + email + ". Feedback" + text + ", Service rating: " + raiting + ", Date: " + date;

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp@gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, "UQtest0!")
            };
            smtpClient.Send(msg);
        }
    }
}
