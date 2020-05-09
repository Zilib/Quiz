using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApp.Model;
using System;

namespace QuizApp.UnitTests
{
    [TestClass]
    public class QuizTests
    {
        private GameConfiguration gameConfiguration = new GameConfiguration(4, 4, 4, 4, "test.txt");

        [TestMethod]
        public void Quiz_0LengthTitle_Should_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Quiz("", gameConfiguration));
        }

        [TestMethod]
        public void Quiz_WhiteSpaceInTitle_Should_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Quiz(" ", gameConfiguration));
        }

        [TestMethod]
        public void Quiz_EmptyTitle_Should_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Quiz(string.Empty, gameConfiguration));
        }

        [TestMethod]
        public void Quiz_ShortTitle_Should_Throw_Exception()
        {
            Assert.ThrowsException<Exception>(() => new Quiz("asa", gameConfiguration));
        }

        [TestMethod]
        public void CreateNewQuestion_EmptyTitle_Should_Throw_ArgumentNullException()
        {
            var quiz = new Quiz("Correct quiz", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => quiz.CreateNewQuestion(string.Empty));
        }

        [TestMethod]
        public void CreateNewQuestion_WhiteSpaceInTitle_Should_Throw_ArgumentNullException()
        {
            var quiz = new Quiz("Correct quiz", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => quiz.CreateNewQuestion(" "));
        }

        [TestMethod]
        public void CreateNewQuestion_0LengthTitle_Should_Throw_ArgumentNullException()
        {
            var quiz = new Quiz("Correct quiz", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => quiz.CreateNewQuestion(""));
        }

        [TestMethod]
        public void CreateNewQuestion_CreateTooMuchQuestions_Should_Throw_ArgumentOutOfRangeException()
        {
            var quiz = new Quiz("Correct quiz", gameConfiguration);
            quiz.CreateNewQuestion("Correct question1");
            quiz.CreateNewQuestion("Correct question2");
            quiz.CreateNewQuestion("Correct question3");
            quiz.CreateNewQuestion("Correct question4");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => quiz.CreateNewQuestion("Incorrect question"));
            Assert.AreEqual(quiz.Questions.Count, 4);
        }

        [TestMethod]
        public void CreateNewQuestion_CorrectQuestions_Should_Return_True()
        {
            var quiz = new Quiz("Correct quiz", gameConfiguration);
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

        [TestMethod]
        public void SetAllDefault_ChangeAllSelectedToFalse_Should_Return_True()
        {
            var quiz = new Quiz("Correct quiz", gameConfiguration);

            var firstQuestion = quiz.CreateNewQuestion("First question");
            firstQuestion.CreateNewAnswer("First answer").IsSelected = true;
            firstQuestion.CreateNewAnswer("Second answer");
            firstQuestion.CreateNewAnswer("Third answer");
            firstQuestion.CreateNewAnswer("Fourth answer");

            Assert.IsTrue(firstQuestion.ExistOneSelectedAnswer());

            var secondQuestion = quiz.CreateNewQuestion("First question");
            secondQuestion.CreateNewAnswer("First answer");
            secondQuestion.CreateNewAnswer("Second answer");
            secondQuestion.CreateNewAnswer("Third answer").IsSelected = true;
            secondQuestion.CreateNewAnswer("Fourth answer");
            Assert.IsTrue(secondQuestion.ExistOneSelectedAnswer());

            quiz.SetAllDefault();
            Assert.IsFalse(firstQuestion.ExistOneSelectedAnswer());
            Assert.IsFalse(secondQuestion.ExistOneSelectedAnswer());
        }
    }
}
