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

        public GameConfiguration GetGameConfiguration() => _quizGame.gameConfiguration;

        public List<Quiz> GetQuizes()
        {
            if (!_quizGame.AnyQuizExist())
            {
                throw new Exception("No quiz exist.");
            }

            return _quizGame.GetAllQuizes();
        }

        public bool IsQuizSelected() => _selectedQuiz != null;

        public bool IsQuestionSelected() => _selectedQuestion != null;

        public bool AnyQuizExist() => _quizGame.AnyQuizExist();
    }
}
