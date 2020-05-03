using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model
{
    public class GameConfiguration
    {
        public readonly int numberOfAnswers;
        public readonly int minQuestions;
        public readonly int maxQuestions;
        public readonly int minTitleLength;
        public readonly string saveFileName;

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
