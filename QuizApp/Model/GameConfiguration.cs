using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model
{
    public class GameConfiguration
    {
        public int numberOfAnswers { get; }
        public int minQuestions { get; }
        public int maxQuestions { get; }
        public int minTitleLength { get; }
        public string saveFileName { get; }

        public GameConfiguration(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            this.numberOfAnswers = numberOfAnswers;
            this.minQuestions = minQuestions;
            this.maxQuestions = maxQuestions;
            this.minTitleLength = minTitleLength;
            this.saveFileName = saveFileName;
        }
    }
}
