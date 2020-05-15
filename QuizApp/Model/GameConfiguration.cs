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

        public override bool Equals(object obj)
        {
            if (obj is GameConfiguration && obj != null)
            {
                GameConfiguration temp;
                temp = (GameConfiguration)obj;
                if (temp.numberOfAnswers == numberOfAnswers && temp.minQuestions == minQuestions 
                    && temp.maxQuestions == maxQuestions && temp.minTitleLength == minTitleLength && temp.saveFileName == saveFileName)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 1117156986;
            hashCode = hashCode * -1521134295 + numberOfAnswers.GetHashCode();
            hashCode = hashCode * -1521134295 + minQuestions.GetHashCode();
            hashCode = hashCode * -1521134295 + maxQuestions.GetHashCode();
            hashCode = hashCode * -1521134295 + minTitleLength.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(saveFileName);
            return hashCode;
        }
    }
}
