using System;
using System.Text.RegularExpressions;

namespace ClassLibrary_Feedback
{
    class Feedback
    {
        
        // Fedback states
        
        private bool IsSent;
        
        // Getting details

        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Text { get; private set; }
        public int Raiting { get; private set; }

        public DateTime Date = DateTime.UtcNow;

        // Email validation
        private bool IsValid(string Email)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(Email, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        /// <summary>
        /// Check the Feedback state
        /// </summary>
        public void Sending()
        {
            _ = UserName.Length <= 20 && UserName != null ? IsSent = true : IsSent = false;
            _ = Raiting <=5 && Raiting >=1 ? IsSent = true : IsSent = false;
            _ = IsSent != true ? "please submit your feedback" : "you have already sent your feedback";
        }
    }
}
