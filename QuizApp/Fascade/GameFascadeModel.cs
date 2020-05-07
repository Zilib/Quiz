using QuizApp.Exceptions;
using QuizApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Fascade
{
    public partial class GameFascade
    {
        private readonly Game _quizGame = null;

        public List<string> Errors { get; private set; }

        public GameConfiguration GetGameConfiguration()
        {
            return _quizGame.gameConfiguration;
        }

        public List<Quiz> GetQuizes(bool setAllDefault)
        {
            if (!_quizGame.Quizes.Any())
            {
                throw new Exception("No quiz exist.");
            }
            if (setAllDefault)
            {
                _quizGame.Quizes.ForEach(x => x.SetAllDefault());
            }
            return _quizGame.Quizes;
        }
    }
}
