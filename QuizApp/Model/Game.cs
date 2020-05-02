using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    partial class Game
    {
        public static int numberOfAnswers { get; } = 4;
        public static int minQuestions { get; } = 2;
        public static int maxQuestions { get; } = 10;
        public static int minTitleLength { get; } = 4;
        public static int minDescriptionLength { get; } = 15;

        private readonly string saveFileName;
        private static bool testsAvailable { get; } = true;


        private Quiz selectedQuiz = null;
        private int playerScore = 0;

        public List<Quiz> Quizes { get; private set; } = new List<Quiz>();

        public Game(string _saveFileName = "Quizes.dat")
        {
            saveFileName = _saveFileName;
            LoadGame();
        }

    }
}
