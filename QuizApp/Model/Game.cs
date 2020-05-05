using QuizApp.Exceptions;
using System;
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

        public Game(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
            quizes = new List<Quiz>();
        }

        public List<Quiz> GetAllQuizes() => quizes;
        public bool AnyQuizExist() => quizes.Count != 0;
        public void AddNewQuiz(Quiz quiz)
        {
            if (quiz == null)
            {
                throw new NullReferenceException();
            }    
            if (quiz.Title == string.Empty)
            {
                throw new IncorrectInputException("Quiz title cannot be null");
            }
            // todo more validation
            quizes.Add(quiz);
        }
        public Quiz GetQuiz(int quizIndex) => quizes[quizIndex];
    }
}
