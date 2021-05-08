using System;
using System.Net.Mail;

namespace ClassLibrary_Feedback
{


    [Serializable]
    class Feedback
    {
        // Feedback state
        private bool _IsSent = false;

        public bool IsSent
        {
            get => _IsSent;
            private set { _IsSent = value; }

        }

        /// Declare parameters
        /// Getting User name with length check (no more than 20 char) and "can not be empty" check
        private string _UserName;
        public string UserName
        {
            get => _UserName;
            set
            {
                if (value == null || value == "" || value.Length > 20) { throw new Exception("too long or empty name"); }
                _UserName = value;
            }
        }

        // Getting Email, system will check email validation itself
        private string _Email;
        public string Email
        {
            get => _Email;
            set => _Email = new MailAddress(value).Address;
        }

        // Getting Message text can be empty
        private string _Text;
        public string Text
        {
            get => _Text;
            set => _Text = value;
        }

        // Getting Raiting with value (1 - 5) check
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

        // Getting date
        private readonly DateTime _Date = DateTime.UtcNow;
        public DateTime Date
        {
            get => _Date;
        }

        // sending the feedback
        public void Send()
        {
            if (Email == null)
                throw new Exception("Email cannot be empty");

            if (UserName == null)
                throw new Exception("UserName cannot be empty");

            if (Raiting == 0)
                throw new Exception("Rating cannot be 0");

            if (IsSent == true)
                throw new Exception("Feedback already sent");

            Feedback checkingFeedback = NetworkManager.GetFeedback(Email);

            if (UserName != checkingFeedback.UserName)
                throw new Exception("Email should be unique");

            if (checkingFeedback.IsSent)
                throw new Exception("Feedback already sent");

            IsSent = true;
            NetworkManager.Send(this);
        }
    }
}
