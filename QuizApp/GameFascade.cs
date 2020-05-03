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

        private readonly Game _quizGame;

        private Quiz selectedQuiz = null;

        private Question selectedQuestion = null;

        public GameFascade(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            Errors = new List<string>();
            _quizGame = new Game(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
        }

        public GameConfiguration GetGameConfiguration() => _quizGame.gameConfiguration;

        public List<Quiz> GetQuizes()
        {
            if (!_quizGame.AnyQuizExist())
            {
                Errors.Add("No quiz exist!");
            }
            return _quizGame.GetAllQuizes();
        }

        public bool IsQuizSelected() => selectedQuiz != null;

        public bool IsQuestionSelected() => selectedQuestion != null;

        public bool AnyQuizExist() => _quizGame.AnyQuizExist();

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
            var questionToCheck = selectedQuiz.GetQuestion(questionIndex).ExistCorrectAnswer();
            return questionToCheck;
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }

        public Quiz CreateNewQuiz(string title)
        {
            if (title.Length < _quizGame.gameConfiguration.minTitleLength)
            {
               throw new System.Exception("Quiz title is not long enought.");
            }

            var newQuiz = new Quiz(title, _quizGame);
            return newQuiz;
        }

        public void AddNewQuiz(Quiz quizToAdd)
        {
            _quizGame.AddNewQuiz(quizToAdd);
        }

        public void SelectQuiz(int quizIndex)
        {
            selectedQuiz = _quizGame.GetQuiz(quizIndex);
        }

        public void SelectQuiz(Quiz quiz)
        {
            if (quiz.CanBeSelected(_quizGame))
            {
                selectedQuiz = quiz;
            }
            else
            {
                throw new System.Exception("Quiz cannot be selected");
            }
        }

        public Question CreateNewQuestion(Quiz selectedQuiz, string questionTitle)
        {
            this.selectedQuiz = selectedQuiz;
            var newQuestion  = this.selectedQuiz.CreateNewQuestion(questionTitle);

            selectedQuestion = newQuestion;
            return newQuestion;
        }

        public void CreateNewQuestion(string questionTitle)
        {
            selectedQuestion = this.selectedQuiz.CreateNewQuestion(questionTitle);
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
