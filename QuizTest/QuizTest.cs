using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApp;
using QuizApp.Model;

namespace QuizTest
{
    [TestClass]
    public class QuizAppTest
    {
        private const int numberOfAnswers = 4;
        private const int minQuestions = 4;
        private const int maxQuestions = 4;
        private const int minTitleLength = 4;

        [TestMethod]
        public void CreateQuizTest()
        {
            GameFascade gameFascade = new GameFascade(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, "Test");

            Assert.ThrowsException<Exception>(() => gameFascade.CreateNewQuiz(""));
            Assert.ThrowsException<Exception>(() => gameFascade.CreateNewQuiz("a"));
            Assert.ThrowsException<Exception>(() => gameFascade.CreateNewQuiz("ab"));
            Assert.ThrowsException<Exception>(() => gameFascade.CreateNewQuiz("abc"));
            Assert.IsInstanceOfType(gameFascade.CreateNewQuiz("abcd"), typeof(Quiz));

            var correctQuiz = gameFascade.CreateNewQuiz("Correct quiz");
            Assert.AreEqual(correctQuiz.Title, "Correct quiz");
            Assert.AreEqual(correctQuiz.Questions.Count, 0);
            Assert.AreEqual(correctQuiz.CanBeSelected(gameFascade.quizGame), true);
        }

        [TestMethod]
        public void CreateQuestionsTest()
        {
            GameFascade gameFascade = new GameFascade(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, "Test");
            GameFascade gameFascade2 = new GameFascade(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, "Test2");

            var correctQuiz = gameFascade2.CreateNewQuiz("Correct quiz");
            // Try to create new question, there where quiz doesn't exist
            Assert.ThrowsException<Exception>(() => gameFascade.CreateNewQuestion(correctQuiz, "Question"));
            Assert.ThrowsException<Exception>(() => gameFascade.CreateNewQuestion(correctQuiz, "Question").GetMyQuiz().CanBeSelected(gameFascade.quizGame));
        }

        [TestMethod]
        public void SelectQuizTest()
        {
            GameFascade gameFascade = new GameFascade(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, "Test");

            var firstQuiz = gameFascade.CreateNewQuiz("First quiz");
            Assert.IsTrue(gameFascade.SelectedQuiz == firstQuiz);

            var secondQuiz = gameFascade.CreateNewQuiz("Second quiz");
            Assert.IsFalse(gameFascade.SelectedQuiz == firstQuiz);
            Assert.IsTrue(gameFascade.SelectedQuiz == secondQuiz);
            gameFascade.AddNewQuiz(firstQuiz);
            gameFascade.AddNewQuiz(secondQuiz);

            Assert.IsFalse(gameFascade.SelectedQuiz == firstQuiz);
            Assert.IsTrue(gameFascade.SelectedQuiz == secondQuiz);
            Assert.AreEqual(gameFascade.GetQuizes().Count, 2);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => gameFascade.SelectQuiz(3));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => gameFascade.SelectQuiz(2));

            gameFascade.SelectQuiz(0);
            Assert.IsTrue(gameFascade.SelectedQuiz == firstQuiz);
            gameFascade.SelectQuiz(secondQuiz);
            Assert.IsTrue(gameFascade.SelectedQuiz == secondQuiz);
        }
    }
}
