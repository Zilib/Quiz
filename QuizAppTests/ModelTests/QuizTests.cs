using NUnit.Framework;
using QuizApp.Models;
using System;

namespace QuizAppTests.UnitTests
{
    [TestFixture]
    public class QuizTests : BaseClass
    {
        private Quiz quiz;

        [SetUp]
        public void QuizSetUp()
        {
            quiz = new Quiz("Quiz example", gameConfiguration);
            quiz.CreateNewQuestion("Correct question1");
            quiz.CreateNewQuestion("Correct question2");
            quiz.CreateNewQuestion("Correct question3");
        }

        [Test]
        public void CreateNewQuestion_EmptyTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => quiz.CreateNewQuestion(string.Empty));
        }

        [Test]
        public void CreateNewQuestion_WhiteSpaceInTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => quiz.CreateNewQuestion(" "));
        }

        [Test]
        public void CreateNewQuestion_0LengthTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => quiz.CreateNewQuestion(""));
        }

        [Test]
        public void CreateNewQuestion_CreateTooMuchQuestions_Should_Throw_Exception()
        {
            quiz.CreateNewQuestion("Correct question4");
            Assert.Throws<Exception>(() => quiz.CreateNewQuestion("Incorrect question"));
            Assert.AreEqual(quiz.Questions.Count, 4);
        }

        [Test]
        public void CreateNewQuestion_CorrectQuestions_Should_Return_True()
        {
            quiz = new Quiz("Correct quiz", gameConfiguration);
            Assert.AreEqual(quiz.Questions.Count, 0);

            quiz.CreateNewQuestion("Correct question1");
            Assert.AreEqual(quiz.Questions.Count, 1);

            quiz.CreateNewQuestion("Correct question2");
            Assert.AreEqual(quiz.Questions.Count, 2);

            quiz.CreateNewQuestion("Correct question3");
            Assert.AreEqual(quiz.Questions.Count, 3);

            quiz.CreateNewQuestion("Correct question4");
            Assert.AreEqual(quiz.Questions.Count, 4);
        }

        [Test]
        public void InsertNewQuestion_CreateTooMuchQuestions_Should_Throw_Exception()
        {
            quiz.CreateNewQuestion("Correct question");
            var newQuestion = new Question("Incorrect question", gameConfiguration);
            Assert.Throws<Exception>(() => quiz.InsertNewQuestion(newQuestion));
        }

        [Test]
        public void InsertNewQuestion_QuestionWithIncorrectCFG_Should_Throw_Exception()
        {
            var config = new GameConfiguration(1, 2, 3, 4, "Test.txt");
            var newQuestion = new Question("Correct question with incorrect game config", config);
            Assert.Throws<Exception>(() => quiz.InsertNewQuestion(newQuestion));
        }

        [Test]
        public void InsertNewQuestion_QuestionWithSimilarCFG_Should_Return_Question()
        {
            var config = new GameConfiguration(4, 4, 4, 4, "test.txt");
            var newQuestion = new Question("Correct question with incorrect game config", config);
            Assert.DoesNotThrow(() => quiz.InsertNewQuestion(newQuestion));
        }

        [Test]
        public void SetAllDefault_ChangeAllSelectedToFalse_Should_Return_True()
        {
            var firstQuestion = quiz.Questions[0];
            firstQuestion.CreateNewAnswer("First answer").IsSelected = true;
            firstQuestion.CreateNewAnswer("Second answer");
            firstQuestion.CreateNewAnswer("Third answer");
            firstQuestion.CreateNewAnswer("Fourth answer");

            Assert.IsTrue(firstQuestion.IsAnyAnswerSelected());

            var secondQuestion = quiz.Questions[1];
            secondQuestion.CreateNewAnswer("First answer");
            secondQuestion.CreateNewAnswer("Second answer");
            secondQuestion.CreateNewAnswer("Third answer").IsSelected = true;
            secondQuestion.CreateNewAnswer("Fourth answer");
            Assert.IsTrue(secondQuestion.IsAnyAnswerSelected());

            quiz.SetAllDefault();
            Assert.IsFalse(firstQuestion.IsAnyAnswerSelected());
            Assert.IsFalse(secondQuestion.IsAnyAnswerSelected());
        }
    }
}