using NUnit.Framework;
using QuizApp.Model;
using QuizApp.UnitTests.pre_test;
using System;

namespace QuizApp.UnitTests.ClassConstructorTests
{
    [TestFixture]
    public class QuestionConstructorTests : BaseClass
    {
        [Test]
        public void Question_0LengthTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Question("", gameConfiguration));
        }

        [Test]
        public void Question_WhiteSpaceInTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Question(" ", gameConfiguration));
        }

        [Test]
        public void Question_EmptyTitle_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Question(string.Empty, gameConfiguration));
        }
    }
}
