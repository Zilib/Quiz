﻿using NUnit.Framework;
using QuizApp.Models;
using System;

namespace QuizAppTests.UnitTests
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