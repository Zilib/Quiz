using QuizApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class GameFascade
    {
        public List<string> Errors { get; private set; }

        public readonly Game quizGame;

        public Quiz SelectedQuiz { get; private set; }

        private Question selectedQuestion = null;

        public GameFascade(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            Errors = new List<string>();
            quizGame = new Game(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
        }

        public GameFascade(GameConfiguration gameConfiguration)
        {
            Errors = new List<string>();
            quizGame = new Game(gameConfiguration);
        }

        public GameConfiguration GetGameConfiguration() => quizGame.gameConfiguration;

        public List<Quiz> GetQuizes()
        {
            if (!quizGame.AnyQuizExist())
            {
                Errors.Add("No quiz exist!");
            }
            return quizGame.GetAllQuizes();
        }

        public bool IsQuizSelected() => SelectedQuiz != null;

        public bool IsQuestionSelected() => selectedQuestion != null;

        public bool AnyQuizExist() => quizGame.AnyQuizExist();

        public bool ExistCorrectAnswer()
        {
            if (selectedQuestion == null)
            {
                throw new System.Exception("Question is not selected");
            }
            return selectedQuestion.ExistCorrectAnswer();
        }

        public bool ExistCorrectAnswer(int questionIndex)
        {
            var questionToCheck = SelectedQuiz.GetQuestion(questionIndex).ExistCorrectAnswer();
            return questionToCheck;
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }

        public Quiz CreateNewQuiz(string title)
        {
            if (title.Length < quizGame.gameConfiguration.minTitleLength)
            {
               throw new System.Exception("Quiz title is not long enought.");
            }

            var newQuiz = new Quiz(title, quizGame);
            SelectedQuiz = newQuiz;

            return newQuiz;
        }

        public void AddNewQuiz(Quiz quizToAdd)
        {
            quizGame.AddNewQuiz(quizToAdd);
        }

        public void SelectQuiz(int quizIndex)
        {
            if (quizIndex >= GetQuizes().Count)
            {
                throw new ArgumentOutOfRangeException("Incorrect quiz index!");
            }
            SelectedQuiz = quizGame.GetQuiz(quizIndex);
        }

        public void SelectQuiz(Quiz quiz)
        {
            if (quiz.CanBeSelected(quizGame) 
                && GetQuizes().Contains(quiz))
            {
                SelectedQuiz = quiz;
            }
            else
            {
                throw new System.Exception("Quiz cannot be selected");
            }
        }

        public Question CreateNewQuestion(Quiz selectedQuiz, string questionTitle)
        {
            if (!GetQuizes().Contains(selectedQuiz))
            {
                throw new Exception("Quiz doesn't exist in pool.");
            }

            this.SelectedQuiz = selectedQuiz;
            var newQuestion  = this.SelectedQuiz.CreateNewQuestion(questionTitle);

            selectedQuestion = newQuestion;
            return newQuestion;
        }

        public Question CreateNewQuestion(string questionTitle)
        {
            if (SelectedQuiz == null)
            {
                throw new Exception("No quiz selected!");
            }

            var newQuestion = this.SelectedQuiz.CreateNewQuestion(questionTitle); ;
            selectedQuestion = newQuestion;
            return newQuestion;
        }

        public void CreateNewAnswer(Question selectedQuestion, string text)
        {
            this.selectedQuestion = selectedQuestion;
            this.selectedQuestion.CreateNewAnswer(text);
        }

        public void CreateNewAnswer(string text)
        {
            selectedQuestion.CreateNewAnswer(text);
        }

    }
}
