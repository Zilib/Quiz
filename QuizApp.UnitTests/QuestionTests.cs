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
            var firstQuestion = new Question("E.g question", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => firstQuestion.CreateNewAnswer(""));
        }

        [TestMethod]
        public void CreateNewAnswer_WhiteSpaceInText_Should_Throw_ArgumentNullException()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => firstQuestion.CreateNewAnswer(" "));
        }

        [TestMethod]
        public void CreateNewAnswer_EmptyText_Should_Throw_ArgumentNullExcepiton()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            Assert.ThrowsException<ArgumentNullException>(() => firstQuestion.CreateNewAnswer(string.Empty));
        }

        [TestMethod]
        public void CreateNewAnswer_CorrectText_Should_Return_True()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            var answerForFirstQuestion = firstQuestion.CreateNewAnswer("Correct answer");
            Assert.IsTrue(firstQuestion.Answers.Contains(answerForFirstQuestion));
        }

        [TestMethod]
        public void CreatenewAnswer_TooMuchAnswers_Should_Throw_IncorrectInputException()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Correct answer2");
            firstQuestion.CreateNewAnswer("Correct answer3");
            firstQuestion.CreateNewAnswer("Correct answer4");
            Assert.ThrowsException<IncorrectInputException>(() => firstQuestion.CreateNewAnswer("Correct answer5"));
        }

        [TestMethod]
        public void SelectAnswer_AnswerFromAnotherQuestion_Should_Return_ArgumentOutOfRangeException()
        {
            var firsQuestion = new Question("E.g question", gameConfiguration);
            var secondQuestion = new Question("E.g question2", gameConfiguration);

            firsQuestion.CreateNewAnswer("Answer1");
            firsQuestion.CreateNewAnswer("Answer2");
            firsQuestion.CreateNewAnswer("Answer3");
            var correctAnswerForFirstQuestion = firsQuestion.CreateNewAnswer("Answer4");

            secondQuestion.CreateNewAnswer("Answer5");
            secondQuestion.CreateNewAnswer("Answer6");
            secondQuestion.CreateNewAnswer("Answer7");
            secondQuestion.CreateNewAnswer("Answer8");
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => secondQuestion.SelectAnswer(correctAnswerForFirstQuestion));
        }

        [TestMethod]
        public void SelectAnswer_CorrectCreatedAnswer_Should_ChangeAnswerIsSelectedValue()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            var answerForFirstQuestion = firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");
            firstQuestion.SelectAnswer(answerForFirstQuestion);
            Assert.IsTrue(answerForFirstQuestion.IsSelected);
        }

        [TestMethod]
        public void SelectAnswer_TwoSelectedAnswers_Should_Throw_Exception()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            var answerForFirstQuestion = firstQuestion.CreateNewAnswer("Correct answer");
            var answer2ForFirstQuestion = firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            firstQuestion.SelectAnswer(answerForFirstQuestion);
            Assert.ThrowsException<Exception>(() => firstQuestion.SelectAnswer(answer2ForFirstQuestion));
        }

        [TestMethod]
        public void ExistOneSelectedAnswer_NoExist_Should_Return_False()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            Assert.IsFalse(firstQuestion.IsAnyAnswerSelected());
        }

        [TestMethod]
        public void ExistOneSelectedAnswer_Exist_Should_Return_True()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.SelectAnswer(firstQuestion.CreateNewAnswer("Answer4"));

            Assert.IsTrue(firstQuestion.IsAnyAnswerSelected());
        }

        [TestMethod]
        public void SelectCorrectAnswer_SelectFromAnotherQuestion_Should_Trow_ArgumentOutOfRangeException()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);
            var secondQuestion = new Question("E.g question2", gameConfiguration);

            firstQuestion.CreateNewAnswer("Answer1");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            secondQuestion.CreateNewAnswer("Answer5");
            secondQuestion.CreateNewAnswer("Answer6");
            secondQuestion.CreateNewAnswer("Answer7");
            var answerFromSecondQuestion = secondQuestion.CreateNewAnswer("Answer8");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => firstQuestion.SetCorrectAnswer(answerFromSecondQuestion));
        }

        [TestMethod]
        public void SelectCorrectAnswer_CreateTwoCorrectAnswers_Should_Throw_ArgumentOutOfRangeException()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);

            firstQuestion.CreateNewAnswer("Answer1");
            firstQuestion.SetCorrectAnswer(firstQuestion.CreateNewAnswer("Answer2"));
            firstQuestion.CreateNewAnswer("Answer3");
            var secondCorrectAnswer = firstQuestion.CreateNewAnswer("Answer4");

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => firstQuestion.SetCorrectAnswer(secondCorrectAnswer));
        }

        [TestMethod]
        public void SelectCorrectAnswer_SetCorrectAnswerTwice_Should_Throw_Exception()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);

            firstQuestion.CreateNewAnswer("Answer1");
            var firstCorrectAnswer = firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            firstQuestion.SetCorrectAnswer(firstCorrectAnswer);
            Assert.ThrowsException<Exception>(() => firstQuestion.SetCorrectAnswer(firstCorrectAnswer));
        }

        [TestMethod]
        public void SetAllAnswersDefault_ChangeSelectedToFalse_Should_Return_True()
        {
            var firstQuestion = new Question("E.g question", gameConfiguration);

            firstQuestion.CreateNewAnswer("Answer1");
            var firstCorrectAnswer = firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            firstQuestion.SelectAnswer(firstCorrectAnswer);
            Assert.IsTrue(firstCorrectAnswer.IsSelected);
            firstQuestion.SetAllAnswersDefault();
            Assert.IsFalse(firstCorrectAnswer.IsSelected);
        }
    }
}
