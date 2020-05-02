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
        private readonly Game _quizGame;
        private Quiz selectedQuiz = null;
        private Question selectedQuestion = null;

        public GameFascade(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            _quizGame = new Game(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
        }

        public GameFascade(Game quizGame)
        {
            _quizGame = quizGame;
        }

        public void CreateNewQuiz(string title)
        {
            try
            {
                selectedQuiz = new Quiz(title);
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
            selectedQuiz = quiz;
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

        public bool ExistCorrectAnswer(int questionIndex)
        {
            var questionToCheck = selectedQuiz.GetQuestion(questionIndex).ExistCorrectAnswer();
            return questionToCheck.
        }
    }
}
