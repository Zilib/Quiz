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

        /*public GameFascade(Game quizGame)
        {
            _quizGame = quizGame;
        }*/

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

        public void CreateNewQuiz(string title)
        {
            try
            {
                selectedQuiz = new Quiz(title, _quizGame);
                _quizGame.AddNewQuiz(selectedQuiz);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        public void CreateNewQuestion(Quiz selectedQuiz, string questionTitle)
        {
            this.selectedQuiz = selectedQuiz;
            selectedQuestion = this.selectedQuiz.CreateNewQuestion(questionTitle);
        }

        public void CreateNewQuestion(string questionTitle)
        {
            selectedQuestion = this.selectedQuiz.CreateNewQuestion(questionTitle);
        }

        public void CreateNewAnswer(string text, bool isCorrect)
        {
            selectedQuestion.CreateNewAnswer(text, isCorrect);
        }


    }
}
