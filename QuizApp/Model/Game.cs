using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Game
    {
        public readonly GameConfiguration gameConfiguration;

        private List<Quiz> quizes;

        public Game(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            gameConfiguration = new GameConfiguration(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
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
            // todo more validation
            quizes.Add(quiz);
        }
        public Quiz GetQuiz(int quizIndex) => quizes[quizIndex];
    }
}
