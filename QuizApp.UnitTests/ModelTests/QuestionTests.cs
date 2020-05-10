using QuizApp.Model;
using QuizApp.Exceptions;
using System;
using QuizApp.UnitTests.pre_test;
using NUnit.Framework;

namespace QuizApp.UnitTests
{
    [TestFixture]
    public class QuestionTests : BaseClass
    {
        private Question firstQuestion;
        private Question secondQuestion;

        [SetUp]
        public void QuestionsSetUp()
        {
            firstQuestion = new Question("E.g question", gameConfiguration);
            secondQuestion = new Question("E.g question2", gameConfiguration);
        }

        [Test]
        public void CreateNewAnswer_0LengthText_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => firstQuestion.CreateNewAnswer(""));
        }

        [Test]
        public void CreateNewAnswer_WhiteSpaceInText_Should_Throw_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => firstQuestion.CreateNewAnswer(" "));
        }

        [Test]
        public void CreateNewAnswer_EmptyText_Should_Throw_ArgumentNullExcepiton()
        {
            Assert.Throws<ArgumentNullException>(() => firstQuestion.CreateNewAnswer(string.Empty));
        }

        [Test]
        public void CreateNewAnswer_CorrectText_Should_Return_True()
        {
            var answerForFirstQuestion = firstQuestion.CreateNewAnswer("Correct answer");
            Assert.IsTrue(firstQuestion.Answers.Contains(answerForFirstQuestion));
        }

        [Test]
        public void CreatenewAnswer_TooMuchAnswers_Should_Throw_IncorrectInputException()
        {
            firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Correct answer2");
            firstQuestion.CreateNewAnswer("Correct answer3");
            firstQuestion.CreateNewAnswer("Correct answer4");
            Assert.Throws<IncorrectInputException>(() => firstQuestion.CreateNewAnswer("Correct answer5"));
        }

        [Test]
        public void SelectAnswer_AnswerFromAnotherQuestion_Should_Return_ArgumentOutOfRangeException()
        {
            firstQuestion.CreateNewAnswer("Answer1");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            var correctAnswerForFirstQuestion = firstQuestion.CreateNewAnswer("Answer4");

            secondQuestion.CreateNewAnswer("Answer5");
            secondQuestion.CreateNewAnswer("Answer6");
            secondQuestion.CreateNewAnswer("Answer7");
            secondQuestion.CreateNewAnswer("Answer8");
            Assert.Throws<ArgumentOutOfRangeException>(() => secondQuestion.SelectAnswer(correctAnswerForFirstQuestion));
        }

        [Test]
        public void SelectAnswer_CorrectCreatedAnswer_Should_ChangeAnswerIsSelectedValue()
        {
            var answerForFirstQuestion = firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");
            firstQuestion.SelectAnswer(answerForFirstQuestion);
            Assert.IsTrue(answerForFirstQuestion.IsSelected);
        }

        [Test]
        public void SelectAnswer_TwoSelectedAnswers_Should_Throw_Exception()
        {
            var answerForFirstQuestion = firstQuestion.CreateNewAnswer("Correct answer");
            var answer2ForFirstQuestion = firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            firstQuestion.SelectAnswer(answerForFirstQuestion);
            Assert.Throws<Exception>(() => firstQuestion.SelectAnswer(answer2ForFirstQuestion));
        }

        [Test]
        public void ExistOneSelectedAnswer_NoExist_Should_Return_False()
        {
            firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            Assert.IsFalse(firstQuestion.IsAnyAnswerSelected());
        }

        [Test]
        public void ExistOneSelectedAnswer_Exist_Should_Return_True()
        {
            firstQuestion.CreateNewAnswer("Correct answer");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.SelectAnswer(firstQuestion.CreateNewAnswer("Answer4"));

            Assert.IsTrue(firstQuestion.IsAnyAnswerSelected());
        }

        [Test]
        public void SelectCorrectAnswer_SelectFromAnotherQuestion_Should_Trow_ArgumentOutOfRangeException()
        {
            firstQuestion.CreateNewAnswer("Answer1");
            firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            secondQuestion.CreateNewAnswer("Answer5");
            secondQuestion.CreateNewAnswer("Answer6");
            secondQuestion.CreateNewAnswer("Answer7");
            var answerFromSecondQuestion = secondQuestion.CreateNewAnswer("Answer8");

            Assert.Throws<ArgumentOutOfRangeException>(() => firstQuestion.SetCorrectAnswer(answerFromSecondQuestion));
        }

        [Test]
        public void SelectCorrectAnswer_CreateTwoCorrectAnswers_Should_Throw_ArgumentOutOfRangeException()
        {
            firstQuestion.CreateNewAnswer("Answer1");
            firstQuestion.SetCorrectAnswer(firstQuestion.CreateNewAnswer("Answer2"));
            firstQuestion.CreateNewAnswer("Answer3");
            var secondCorrectAnswer = firstQuestion.CreateNewAnswer("Answer4");

            Assert.Throws<ArgumentOutOfRangeException>(() => firstQuestion.SetCorrectAnswer(secondCorrectAnswer));
        }

        [Test]
        public void SelectCorrectAnswer_SetCorrectAnswerTwice_Should_Throw_Exception()
        {
            firstQuestion.CreateNewAnswer("Answer1");
            var correctAnswer = firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            firstQuestion.SetCorrectAnswer(correctAnswer);
            Assert.Throws<Exception>(() => firstQuestion.SetCorrectAnswer(correctAnswer));
        }

        [Test]
        public void SetAllAnswersDefault_ChangeSelectedToFalse_Should_Return_True()
        {
            firstQuestion.CreateNewAnswer("Answer1");
            var correctAnswer = firstQuestion.CreateNewAnswer("Answer2");
            firstQuestion.CreateNewAnswer("Answer3");
            firstQuestion.CreateNewAnswer("Answer4");

            firstQuestion.SelectAnswer(correctAnswer);
            Assert.IsTrue(correctAnswer.IsSelected);
            firstQuestion.SetAllAnswersDefault();
            Assert.IsFalse(correctAnswer.IsSelected);
        }
    }
}
