using NUnit.Framework;
using QuizApp.Models;
using System;

namespace QuizAppTests.UnitTests
{
    [TestFixture]
    public class GameTests : BaseClass
    {
        private Game game;

        [SetUp]
        public void GamesSetUp()
        {
            game = new Game(gameConfiguration);
        }

        [Test]
        public void CreateNewQuiz_ShortTitle_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => game.CreateNewQuiz("sh"));
        }

        [Test]
        public void GetQuizes_NoQuizExist_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => game.GetQuizes());
        }

        public void GetQuizes_SetAllDefaultTrue_Should_Return_True()
        {
            var quiz = game.CreateNewQuiz("Correct title");

            var firstQuestion = quiz.CreateNewQuestion("First question!");

        }
    }
}