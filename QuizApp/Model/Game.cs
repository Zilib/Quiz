using System.Collections.Generic;
using QuizApp.Model;

namespace QuizApp.Model
{
    public class Game
    {
        public readonly int numberOfAnswers;
        public readonly int minQuestions;
        public readonly int maxQuestions;
        public readonly int minTitleLength;
        public readonly string saveFileName;

        public List<Quiz> Quizes { get; set; }

        public Game(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            this.numberOfAnswers = numberOfAnswers;
            this.minQuestions = minQuestions;
            this.maxQuestions = maxQuestions;
            this.minTitleLength = minTitleLength;
            this.saveFileName = saveFileName;
            Quizes = new List<Quiz>();
        }
    }
}
