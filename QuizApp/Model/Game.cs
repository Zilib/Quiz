using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public class Game
    {
        private Quiz selectedQuiz = null;
        private Question selectedQuestion = null;

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

        public bool AnyQuizExist() => quizes.Any();

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
            selectedQuiz = quizes.Last();
        }

        public Quiz GetQuiz(int quizIndex) => quizes[quizIndex];

        public void GetCurrentQuizRef(ref Quiz quiz)
        {
            quiz = selectedQuiz;
        }

        public Question GetCurrentQuestion() => selectedQuestion;

        public void SelectQuiz(Quiz quizToSelect)
        {
            if (!quizToSelect.CanBeSelected(this) && quizes.Contains(quizToSelect))
            {
                throw new Exception("Quiz cannot be selected");
            }

            selectedQuiz = quizToSelect;
            selectedQuestion = selectedQuiz.GetQuestion(0);
        }

        public Quiz CreateNewQuiz(string title)
        {
            if (title.Length < gameConfiguration.minTitleLength)
            {
                throw new IncorrectInputException("Quiz title is not long enought.");
            }

            selectedQuiz = new Quiz(title, this);
            quizes.Add(selectedQuiz);

            return selectedQuiz;
        }

        public bool RemoveQuiz(Quiz quizToRemove)
        {
            if (quizes.Contains(quizToRemove))
            {
                quizes.Remove(quizToRemove);
                return true;
            }
            return false;
        }
    }
}
