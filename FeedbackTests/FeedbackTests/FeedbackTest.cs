using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary_Feedback;
using System;
using System.Threading;

namespace FeedbackTests
{
    [TestClass]
    public class FeedbackTest
    {

        /// List of checking parameters
        /// 
        enum Skip
        {
            None,
            Name,
            Email,
            Rating,
            Feedback
        }
        
        /// Random string generator
        /// 
        string GenerateString(int lenght)
        {
            var rnd = new Random();
            char[] str = new char[lenght];
            for (int i = 0; i < lenght; i++)
                if (rnd.Next(2) > 0)
                    str[i] = (char)rnd.Next('A', 'Z' + 1);
                else
                    str[i] = (char)rnd.Next('a', 'z' + 1);
            return new string(str);
        }

        /// Random email generator
        /// 
        string GenerateRandomEmail()
        {
            var rnd = new Random();
            string email = GenerateString(rnd.Next(1, 10)) + "@" + GenerateString(rnd.Next(1, 10)) + ".com";
            return email;
        }
        
        /// Feedback generator
        /// 
        Feedback GenerateFeedback(Skip skip = Skip.None)
        {
            var feedback = new Feedback();

            if (skip != Skip.Name)
                feedback.UserName = GenerateString(15);

            if (skip != Skip.Email)
                feedback.Email = GenerateRandomEmail();

            if (skip != Skip.Rating)
                feedback.Raiting = 5;

            if (skip != Skip.Feedback)
                feedback.Text =  GenerateString(200);

            return feedback;
        }

        ///Feedback normal submition test
        ///
        [TestMethod]
        public void Feedback_NormalSubmition()
        {
            //arrange
            var feedback = GenerateFeedback();
            //act
            feedback.Send();
            //assert

        }

        [TestMethod]
        public void Test_UserName_LenghtZero()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Name);

            //act
            try
            {
                feedback.UserName = "";
            }
            catch (NullReferenceException ex)
            {
                if (ex.Message.Contains("Name") == false)
                    throw ex;
                return; //exception went from UserName, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_UserName_Null()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Name);

            //act
            try
            {
                feedback.Send();
            }
            catch (NullReferenceException ex)
            {
                if (ex.Message.Contains("UserName") == false)
                    throw ex;
                return; //exception went from UserName, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_UserName_NonEnglish()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Name);

            //act
            try
            {
                feedback.UserName = "Non-English chars";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("english") == false)
                    throw ex;
                return; //exception went from UserName, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_UserName_LenghtTooLong()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Name);

            //act
            try
            {
                feedback.UserName = GenerateString(22);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Name") == false)
                    throw ex;
                return; //exception went from UserName, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_Email_IsNull()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Email);

            //act
            try
            {
                feedback.Send();
            }
            catch (NullReferenceException ex)
            {
                if (ex.Message.Contains("Email") == false)
                    throw ex;
                return; //exception went from Email, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_Email_IsNonValid()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Email);

            //act
            try
            {
                feedback.Email = "!!!@@@!!!";
            }
            catch (Exception) //exception went from Email, it is normal behavior
            {
                return;
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_Email_SecondSubmission()
        {
            //arrange
            var feedback = GenerateFeedback();
            feedback.Send();
            try
            {
                feedback.Send();
            }
            catch (Exception ex)
            {
                if (ex.Message != Feedback.AlreadySentException)
                    throw ex;
                return; //exception went from Email, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_Email_SecondSubmissionByUser()
        {
            //arrange
            var feedback = GenerateFeedback();
            feedback.Send();
            Thread.Sleep(500); //this pause is nessesary for letting hdd to work with tests

            var nonUniquie = GenerateFeedback();
            nonUniquie.Email = feedback.Email;
            nonUniquie.UserName = feedback.UserName;

            try
            {
                nonUniquie.Send();
            }
            catch (Exception ex) //User can send only one feedback
            {
                if (ex.Message != Feedback.AlreadySentException)
                    throw ex;
                return; 
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_Email_NotUnquie()
        {
            //arrange
            var feedback = GenerateFeedback();
            feedback.Send();
            Thread.Sleep(500);


            var nonUniquie = GenerateFeedback();
            nonUniquie.Email = feedback.Email;
            try
            {
                nonUniquie.Send();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Email") == false)
                    throw ex;
                return; //exception went from Email, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }

        [TestMethod]
        public void Test_Raiting_NotInRange()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Rating);

            //act
            try
            {
                feedback.Send();
            }
            catch (NullReferenceException ex)
            {
                if (ex.Message.Contains("Raiting") == false)
                    throw ex;
                return; //exception went from Raiting, it is normal behavior
            }

            //assert
            Assert.Fail("No exception was catch");
        }
    }
}
