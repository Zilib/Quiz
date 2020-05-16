using NUnit.Framework;
using QuizApp.Models;

namespace QuizAppTests.UnitTests
{
    [TestFixture]
    public class AnswerTests
    {
        private Answer answer;

        [SetUp]
        public void AnswersSetUp()
        {
            answer = new Answer("Answer number one");
        }

        [Test]
        public void GetState_SelectedAndCorrect_Should_Return_Correct()
        {
            answer.IsSelected = true;
            answer.IsCorrect = true;
            Assert.AreEqual(answer.GetState(), EAnswerState.Correct);
        }

        [Test]
        public void GetState_SelectedAndIncorrect_Should_Return_Incorrect()
        {
            answer.IsSelected = true;
            answer.IsCorrect = false;
            Assert.AreEqual(answer.GetState(), EAnswerState.Incorrect);
        }

        [Test]
        public void GetState_UnselectedAndCorrect_Should_Return_Null()
        {
            answer.IsSelected = false;
            answer.IsCorrect = false;
            Assert.AreEqual(answer.GetState(), null);
        }
    }
}