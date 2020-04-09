using System;
using System.Collections.Generic;

namespace QuizApp
{
    class Game
    {
        #region Private variables

        // Number of all quizes
        private List<Quiz> Quizes = new List<Quiz>();

        #endregion

        #region Public methods 

        public int GetNumberOfQuizes => Quizes.Count;

        #endregion
    }

    class Quiz
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
