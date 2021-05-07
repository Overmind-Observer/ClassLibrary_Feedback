using System;
using System.Net;
using System.Net.Mail;

namespace ClassLibrary_Feedback
{


    
    class Feedback
    {
        // Feedback state
        private bool IsSent = false;

        // Declair parameters
        // User name with lenght check (no more than 20 char) and can not be empty check
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set => _ = _UserName == "" || _UserName.Length > 20 ? throw new Exception("too long or empty name") :
            _UserName = value;
        }
        // Email with email validation (in progress)
        private string _Email;
        public string Email
        {
            get => _Email;


            set
            {
                bool IsValidEmail(string _Email)
                {
                    MailAddress addr = new MailAddress(_Email);
                    return addr.Address == _Email;
                }
            }
        }
        // Message text can be empty
        private string _Text;
        public string Text
        {
            get => _Text;
            set => _Text = "";
        }

        // Raiting with value (1 - 5) check
        private int _Raiting;

        public int Raiting
        {
            get => _Raiting;
            set => _ = _Raiting < 1 || _Raiting > 5 ? throw new Exception("incorrect value of Raiting") :
                _Raiting = value;
        }
        // Getting date
        private DateTime _Date;  
        public DateTime Date { 
            get => _Date;
            set => _Date = DateTime.Now;
        } // not sure about this statement !?


        // sending the feedback

        public void Send()
        {
            MailAddress from = new MailAddress("UltimateQuarry@gmail.com");
            MailAddress to = new MailAddress("UQ_SupporTeam@gmail.com");
            MailMessage message = new MailMessage(from, to)
            {
                Body = "From: " + UserName + ", Contact Email:" + Email
                + ". Feedback" + Text + ", Service rating: " + Raiting
                + ", Date: " + Date
            };

            SmtpClient smtpClient = new SmtpClient
            {
                Host = "smtp@gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, "UQtest0!")
            };

            try
            {
                smtpClient.Send(message); // will it work ?
                IsSent = true;
            }
            catch
            {
                throw new Exception("your message was not send");
            }
           
        }
    }
}
