using Microsoft.VisualStudio.TestTools.UnitTesting;
using Faker;
using ClassLibrary_Feedback;

namespace FeedbackTests
{
    [TestClass]
    public class FeedbackTest
    {
        [TestMethod]
        public void Test_Feedback_Submition()
        {
            //arrange
            string email = Internet.Email();
            //act
            NetworkManager sender = new NetworkManager();


            //assert

        }
        public void Test_UserName_Lenght()
        {
            //arrange
            //act
            //assert
        }
        public void Test_UserName_NotEmpty()
        {
            //arrange
            //act
            //assert
        }
        public void Test_Email_IsValid()
        {
            //arrange
            //act
            //assert
        }
        public void Test_Raiting_Range()
        {
            //arrange
            //act
            //assert
        }
    }
}
