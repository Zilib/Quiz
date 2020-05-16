using NUnit.Framework;
using QuizApp.Models;
using System;

namespace QuizAppTests.UnitTests
{
    [TestFixture]
    public class AnswerConstructorTests
    {
        [Test]
        public void Answer_0LengthTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Answer(""));
        }

        [Test]
        public void Answer_WhiteSpaceInTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Answer(" "));
        }

        [Test]
        public void Answer_EmptyTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Answer(string.Empty));
        }
    }
}