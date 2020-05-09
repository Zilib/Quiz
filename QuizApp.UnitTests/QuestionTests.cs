using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizApp.Model;
using QuizApp.Exceptions;
using System;

namespace QuizApp.UnitTests
{
    [TestClass]
    public class QuestionTests
    {
        private GameConfiguration gameConfiguration = new GameConfiguration(4, 4, 5, 6, "test.txt");

        [TestMethod]
        public void Question_0LengthTitle_Should_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Question("", gameConfiguration));
        }

        [TestMethod]
        public void Question_WhiteSpaceInTitle_Should_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Question(" ", gameConfiguration));
        }

        [TestMethod]
        public void Question_EmptyTitle_Should_Throw_ArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new Question(string.Empty, gameConfiguration));
        }

        [TestMethod]
        public void CreateNewAnswer_0LengthText_Should_Throw_ArgumentNullException()
        {
            var question = new Question("E.g question", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => question.CreateNewAnswer(""));
        }

        [TestMethod]
        public void CreateNewAnswer_WhiteSpaceInText_Should_Throw_ArgumentNullException()
        {
            var question = new Question("E.g question", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => question.CreateNewAnswer(" "));
        }

        [TestMethod]
        public void CreateNewAnswer_EmptyText_Should_Throw_ArgumentNullExcepiton()
        {
            var question = new Question("E.g question", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => question.CreateNewAnswer(string.Empty));
        }

        [TestMethod]
        public void CreateNewAnswer_CorrectText_Should_Return_True()
        {
            var question = new Question("E.g question", gameConfiguration);
            var answer = question.CreateNewAnswer("Correct answer");
            Assert.IsTrue(question.Answers.Contains(answer));
        }

        [TestMethod]
        public void CreatenewAnswer_TooMuchAnswers_Should_Throw_IncorrectInputException()
        {
            var question = new Question("E.g question", gameConfiguration);
            question.CreateNewAnswer("Correct answer");
            question.CreateNewAnswer("Correct answer2");
            question.CreateNewAnswer("Correct answer3");
            question.CreateNewAnswer("Correct answer4");
            Assert.ThrowsException<IncorrectInputException>(() => question.CreateNewAnswer("Correct answer5"));
        }

        [TestMethod]
        public void SelectAnswer_AnswerFromAnotherQuestion_Should_Return_ArgumentOutOfRangeException()
        {
            var question = new Question("E.g question", gameConfiguration);
            var question2 = new Question("E.g question2", gameConfiguration);

            question.CreateNewAnswer("Answer1");
            question.CreateNewAnswer("Answer2");
            question.CreateNewAnswer("Answer3");
            var correctAnswer = question.CreateNewAnswer("Answer4");

            question2.CreateNewAnswer("Answer5");
            question2.CreateNewAnswer("Answer6");
            question2.CreateNewAnswer("Answer7");
            question2.CreateNewAnswer("Answer8");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => question2.SelectAnswer(correctAnswer));
        }

        [TestMethod]
        public void SelectAnswer_CorrectCreatedAnswer_Should_ChangeAnswerIsSelectedValue()
        {
            var question = new Question("E.g question", gameConfiguration);
            var answer = question.CreateNewAnswer("Correct answer");
            question.CreateNewAnswer("Answer2");
            question.CreateNewAnswer("Answer3");
            question.CreateNewAnswer("Answer4");
            question.SelectAnswer(answer);
            Assert.IsTrue(answer.IsSelected);
        }

        [TestMethod]
        public void SelectAnswer_TwoSelectedAnswers_Should_Throw_Exception()
        {
            var question = new Question("E.g question", gameConfiguration);
            var answer = question.CreateNewAnswer("Correct answer");
            var answer2 = question.CreateNewAnswer("Answer2");
            question.CreateNewAnswer("Answer3");
            question.CreateNewAnswer("Answer4");

            question.SelectAnswer(answer);
            Assert.ThrowsException<Exception>(() => question.SelectAnswer(answer2));
        }

        [TestMethod]
        public void ExistOneSelectedAnswer_NoExist_Should_Return_False()
        {
            var question = new Question("E.g question", gameConfiguration);
            question.CreateNewAnswer("Correct answer");
            question.CreateNewAnswer("Answer2");
            question.CreateNewAnswer("Answer3");
            question.CreateNewAnswer("Answer4");

            Assert.IsFalse(question.ExistOneSelectedAnswer());
        }

        [TestMethod]
        public void ExistOneSelectedAnswer_Exist_Should_Return_True()
        {
            var question = new Question("E.g question", gameConfiguration);
            question.CreateNewAnswer("Correct answer");
            question.CreateNewAnswer("Answer2");
            question.CreateNewAnswer("Answer3");
            var answer = question.CreateNewAnswer("Answer4");

            question.SelectAnswer(answer);

            Assert.IsTrue(question.ExistOneSelectedAnswer());
        }
    }
}
