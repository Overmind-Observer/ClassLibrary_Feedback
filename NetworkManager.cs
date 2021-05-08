using System.Text.Json;
using System.IO;

namespace ClassLibrary_Feedback
{
    /// <summary>
    /// The class for storing and receiving the users' data.
    /// business requirements: 
    /// 1. Sending feedback somewhere for keeping it.
    /// 2. Receiving feedback by email(the only email should be unique).
    /// For testing purpose, both requirements realized by JSON serialization.
    /// </summary>
    public class NetworkManager
    {

        static string GetPath(string email)
        {
            return email + ".json";
        }


        public static void Send(Feedback feedback)
        {
            string jsonString = JsonSerializer.Serialize(feedback);
            File.WriteAllText(GetPath(feedback.Email), jsonString);
        }


        public static Feedback GetFeedback(string email)
        {
            if (File.Exists(email))
                return JsonSerializer.Deserialize<Feedback>(File.ReadAllText(GetPath(email)));

            return null;
        }
    }
    
}
