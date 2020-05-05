using QuizApp.Exceptions;
using QuizApp.Model;
using System;
using System.Collections.Generic;

namespace QuizApp.Fascade
{
    public partial class GameFascade
    {
        private Question selectedQuestion = null;
        public readonly Game quizGame;

        public Quiz SelectedQuiz { get; private set; }
        public List<string> Errors { get; private set; }
        public GameConfiguration GetGameConfiguration() => quizGame.gameConfiguration;
        public List<Quiz> GetQuizes()
        {
            if (!quizGame.AnyQuizExist())
            {
                throw new Exception("No quiz exist.");
            }

            return quizGame.GetAllQuizes();
        }

        public bool IsQuizSelected() => SelectedQuiz != null;

        public bool IsQuestionSelected() => selectedQuestion != null;

        public bool AnyQuizExist() => quizGame.AnyQuizExist();

        public bool ExistCorrectAnswer()
        {
            if (!IsQuizSelected())
            {
                throw new QuizIsNotSelectedException();
            }

            if (!IsQuestionSelected())
            {
                throw new Exception("Question is not selected!");
            }

            return selectedQuestion.ExistCorrectAnswer();
        }

        public bool ExistCorrectAnswer(int questionIndex)
        {
            if (!IsQuizSelected())
            {
                throw new QuizIsNotSelectedException();
            }

            var doesQuestionHasCorrectAnswer = SelectedQuiz.GetQuestion(questionIndex).ExistCorrectAnswer();

            return doesQuestionHasCorrectAnswer;
        }


    }
}
