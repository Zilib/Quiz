using System;
using QuizApp.Model;
using QuizApp.UnitTests.pre_test;
using NUnit.Framework;

namespace QuizApp.UnitTests
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
    }
}
