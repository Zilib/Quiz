using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    partial class Game
    {
        #region Config variables

        public static int numberOfAnswers { get; } = 4;
        public static int minQuestions { get; } = 2;
        public static int maxQuestions { get; } = 10;
        public static int minTitleLength { get; } = 4;
        public static int minDescriptionLength { get; } = 15;

        #endregion

        private readonly string saveFileName;
        private static bool testsAvailable { get; } = true;

        // Reffer to selected quiz
        private Quiz selectedQuiz = null;
        // Not used right now
        private int playerScore = 0;

        // All quizes
        public List<Quiz> Quizes { get; private set; } = new List<Quiz>();

        #region Constructs

        public Game(string _saveFileName = "Quizes.dat")
        {
            saveFileName = _saveFileName;
            LoadGame();
        }

        #endregion
    }
}
