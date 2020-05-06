using QuizApp.Exceptions;
using QuizApp.Model;
using System;
using System.Collections.Generic;

namespace QuizApp.Fascade
{
    public partial class GameFascade
    {
        private readonly Game _quizGame = null;
        private Question _selectedQuestion = null;
        private Quiz _selectedQuiz = null;

        public List<string> Errors { get; private set; }

        public GameConfiguration GetGameConfiguration()
        {
            return _quizGame.gameConfiguration;
        }

        public List<Quiz> GetQuizes()
        {
            if (!_quizGame.AnyQuizExist())
            {
                throw new Exception("No quiz exist.");
            }

            return _quizGame.GetAllQuizes();
        }

        public bool IsQuizSelected() 
        {
            return _selectedQuiz != null;
        }

        public bool IsQuestionSelected() 
        {
            return _selectedQuestion != null;
        }

        public bool AnyQuizExist()
        {
           return _quizGame.AnyQuizExist();
        }

        public Quiz GetCurrentQuiz()
        {
            if (!IsQuizSelected())
            {
                throw new NullReferenceException();
            }
            return _selectedQuiz;
        }
    }
}
