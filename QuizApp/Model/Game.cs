using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public class Game
    {
        public Quiz selectedQuiz { get; private set; }

        public readonly GameConfiguration gameConfiguration;

        public List<Quiz> Quizes { get; private set; }

        public Game(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            gameConfiguration = new GameConfiguration(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
            Quizes = new List<Quiz>();
        }

        public Game(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
            Quizes = new List<Quiz>();
        }

        public Quiz GetCurrentQuiz()
        {
            return selectedQuiz;
        }

        public Quiz SelectQuiz(Quiz quizToSelect)
        {
            if (!quizToSelect.CanBeSelected(this) && Quizes.Contains(quizToSelect))
            {
                throw new Exception("Quiz cannot be selected");
            }

            selectedQuiz = quizToSelect;

            return selectedQuiz;
        }

        public Quiz CreateNewQuiz(string title)
        {
            if (title.Length < gameConfiguration.minTitleLength)
            {
                throw new IncorrectInputException("Quiz title is not long enought.");
            }

            selectedQuiz = new Quiz(title, this);
            Quizes.Add(selectedQuiz);

            return selectedQuiz;
        }

        public bool RemoveQuiz(Quiz quizToRemove)

        {
            if (Quizes.Contains(quizToRemove))
            {
                Quizes.Remove(quizToRemove);
                return true;
            }
            return false;
        }
    }
}
