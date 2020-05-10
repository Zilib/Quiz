using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QuizApp.Model;

namespace QuizApp.UnitTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void CreateNewQuiz_ShortTitle_Should_Throw_Exception()
        {
            var Game = new Game(4, 4, 4, 4, "test.txt");

            Assert.ThrowsException<Exception>(() => Game.CreateNewQuiz("sh"));
        }

        [TestMethod]
        public void GetQuizes_NoQuizExist_Should_Throw_Exception()
        {
            var game = new Game(4, 4, 4, 4, "test.txt");

            Assert.ThrowsException<Exception>(() => game.GetQuizes());
        }
    }
}
