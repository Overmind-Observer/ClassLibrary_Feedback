using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary_Feedback;
using System;

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

        /// Random name generator
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
            string email = GenerateString(rnd.Next(10)) + "@" + GenerateString(rnd.Next(10)) + ".net";
            return email;
        }

        /// Feedback generator
        /// 
        Feedback GenerateFeedback(Skip skip = Skip.None)
        {
            var feedback = new Feedback();

            if (skip != Skip.Name)
                feedback.UserName = GenerateString(10);

            if (skip != Skip.Email)
                feedback.Email = GenerateRandomEmail();

            if (skip != Skip.Rating)
                feedback.Raiting = 5;

            if (skip != Skip.Feedback)
                feedback.Text = GenerateString(200);

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
                feedback.Send();
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
            catch (Exception ex)
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
        public void Test_UserName_LenghtTooLong()
        {
            //arrange
            var feedback = GenerateFeedback(Skip.Name);

            //act
            try
            {
                feedback.Send();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UserName") == false)
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
        public void Test_Email_SecondSubmission()
        {
            //arrange
            //act
            //assert
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
