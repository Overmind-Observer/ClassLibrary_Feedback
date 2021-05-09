using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ClassLibrary_Feedback
{


    [Serializable]
    public class Feedback
    {
        /// Feedback state
        private bool _IsSent = false;

        public bool IsSent
        {
            get => _IsSent;
            private set { _IsSent = value; }

        }

        /// Declare parameters
        /// Getting User name with ckecks of length (no more than 20 char), "can not be empty" and english charasters.
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {

                if (value == null || value == "") { throw new NullReferenceException("Name can not be empty or null"); }

                if (value.Length > 20) { throw new Exception("Name is too long"); }

                Regex regex = new Regex("^[a-z A-Z ]*");

                if (regex.IsMatch(value)) 
                { 
                    throw new Exception("Name has to contain only english charasters " + value) ; 
                }

                _UserName = value;
            }
        }

        /// Getting Email, system will check email validation itself
        private string _Email;
        public string Email
        {
            get => _Email;
            set {
                try
                {
                    _Email = new MailAddress(value).Address;
                }catch(Exception ex)
                {
                    throw new Exception(ex.Message + " " + value);
                }
            }
        }

        /// Getting Message text can be empty
        private string _Text;
        public string Text
        {
            get => _Text;
            set => _Text = value;
        }

        /// Getting Raiting with value (1 - 5) check
        private int _Raiting = 0;

        public int Raiting
        {
            get => _Raiting;
            set
            {
                if (value < 1 || value > 5) { throw new Exception("incorrect value of Raiting"); }
                _Raiting = value;
            }

        }

        /// Getting date
        private readonly DateTime _Date = DateTime.UtcNow;
        public DateTime Date
        {
            get => _Date;
        }

        /// Sending the feedback
        /// 
        /// Empty credentials and feedback's state check
        /// 
        public void Send()
        {
            if (Email == null)
                throw new NullReferenceException("Email cannot be empty");

            if (UserName == null)
                throw new NullReferenceException("UserName cannot be empty");

            if (Raiting == 0)
                throw new NullReferenceException("Raiting cannot be 0");

            if (IsSent == true)
                throw new NullReferenceException("Feedback already sent");

            ///Cheking feedback conditions for getting feedback state
            ///
            Feedback checkingFeedback = NetworkManager.GetFeedback(Email);

            if (checkingFeedback != null)
            {
                if (UserName != checkingFeedback.UserName)
                    throw new Exception("Email should be unique"); //is it work correct?

                if (checkingFeedback.IsSent)
                    throw new Exception("Feedback already sent");
            }

            IsSent = true;
            NetworkManager.Send(this);
        }
    }
}
