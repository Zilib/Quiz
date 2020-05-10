using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public class Game
    {
        public GameConfiguration gameConfiguration { get; }

        private List<Quiz> quizes = new List<Quiz>();

        public Game(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            gameConfiguration = new GameConfiguration(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
        }

        public Game(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }

        public Quiz CreateNewQuiz(string title)
        {
            var newQuiz = new Quiz(title, gameConfiguration);
            quizes.Add(newQuiz);

            return newQuiz;
        }

        public List<Quiz> GetQuizes(bool setAllDefault = false)
        {
            if (!quizes.Any())
            {
                throw new Exception("No quiz exist.");
            }
            if (setAllDefault)
            {
                quizes.ForEach(x => x.SetAllDefault());
            }
            return quizes;
        }

        public void RemoveQuiz(Quiz quizToRemove)

        {
            if (!quizes.Contains(quizToRemove))
            {
                throw new ArgumentOutOfRangeException();
            }
            quizes.Remove(quizToRemove);
        }
    }
}
