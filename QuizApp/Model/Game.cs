using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Game
    {
        public readonly int numberOfAnswers;
        public readonly int minQuestions;
        public readonly int maxQuestions;
        public readonly int minTitleLength;
        public readonly string saveFileName;

        private List<Quiz> quizes;

        public Game(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            this.numberOfAnswers = numberOfAnswers;
            this.minQuestions = minQuestions;
            this.maxQuestions = maxQuestions;
            this.minTitleLength = minTitleLength;
            this.saveFileName = saveFileName;
            quizes = new List<Quiz>();
        }

        public List<Quiz> GetAllQuizes() => quizes;
        public bool AnyQuizExist() => quizes.Count != 0;
        public void AddNewQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                throw new System.Exception("Quiz cannot be null");
            }    
            if (quiz.Title == string.Empty)
            {
                throw new System.Exception("Quiz title cannot be null");
            }
            quizes.Add(quiz);
        }
        public Quiz GetQuiz(int quizIndex) => quizes[quizIndex];
    }
}
