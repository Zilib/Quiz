using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApp.Model;

namespace QuizApp.UnitTests
{
    [TestClass]
    public class AnswerTests
    {
        [TestMethod]
        public void GetState_SelectedAndCorrect_Should_Return_Correct()
        {
            var answer = new Answer("Answer number one");
            answer.IsSelected = true;
            answer.IsCorrect = true;
            Assert.AreEqual(answer.GetState(), EAnswerState.Correct);
        }

        [TestMethod]
        public void GetState_SelectedAndIncorrect_Should_Return_Incorrect()
        {
            var answer = new Answer("Answer number one");
            answer.IsSelected = true;
            answer.IsCorrect = false;
            Assert.AreEqual(answer.GetState(), EAnswerState.Incorrect);
        }

        [TestMethod]
        public void GetState_UnselectedAndCorrect_Should_Return_Null()
        {
            var answer = new Answer("Answer number one");
            answer.IsSelected = false;
            answer.IsCorrect = false;
            Assert.AreEqual(answer.GetState(), null);
        }
    }
}
