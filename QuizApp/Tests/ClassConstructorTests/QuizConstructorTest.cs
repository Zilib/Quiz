using NUnit.Framework;
using QuizApp.Model;
using QuizApp.UnitTests.pre_test;
using System;


namespace QuizApp.UnitTests
{
    [TestFixture]
    public class QuizConstructorTest : BaseClass
    {
        [Test]
        public void Quiz_0LengthTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Quiz("", gameConfiguration));
        }

        [Test]
        public void Quiz_WhiteSpaceInTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Quiz(" ", gameConfiguration));
        }

        [Test]
        public void Quiz_EmptyTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Quiz(string.Empty, gameConfiguration));
        }

        [Test]
        public void Quiz_ShortTitle_Should_Throw_Exception()
        {
            Assert.Throws<Exception>(() => new Quiz("asa", gameConfiguration));
        }
    }
}
