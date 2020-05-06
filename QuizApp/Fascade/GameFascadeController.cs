using QuizApp.Exceptions;
using QuizApp.Model;
using System;
using System.Collections.Generic;

namespace QuizApp.Fascade
{
    public partial class GameFascade
    {
        public GameFascade(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            Errors = new List<string>();
            _quizGame = new Game(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
            _quizGame.GetCurrentQuizRef(ref _selectedQuiz);
        }

        public GameFascade(GameConfiguration gameConfiguration)
        {
            Errors = new List<string>();
            _quizGame = new Game(gameConfiguration);
            _quizGame.GetCurrentQuizRef(ref _selectedQuiz);
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }

        public void CreateNewQuiz(string title)
        {
            _selectedQuiz = _quizGame.CreateNewQuiz(title);
        }

        public void AddNewQuiz(Quiz quizToAdd)
        {
            _quizGame.AddNewQuiz(quizToAdd);
        }

        public void SelectQuiz(int quizIndex)
        {
            try
            {
                var quizToSelect = _quizGame.GetQuiz(quizIndex);
                _quizGame.SelectQuiz(quizToSelect);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                return;
            }
        }

        public void SelectQuiz(Quiz quiz)
        {
            _quizGame.SelectQuiz(quiz);
        }

        public Question CreateNewQuestion(Quiz selectedQuiz, string questionTitle)
        {
            if (!GetQuizes().Contains(selectedQuiz))
            {
                throw new ArgumentOutOfRangeException();
            }

            var newQuestion = _selectedQuiz.CreateNewQuestion(questionTitle);
            return newQuestion;
        }

        public Question CreateNewQuestion(string questionTitle)
        {
            if (_selectedQuiz == null)
            {
                throw new QuizIsNotSelectedException();
            }

            var newQuestion = _selectedQuiz.CreateNewQuestion(questionTitle);
            return newQuestion;
        }

        public void CreateNewAnswer(Question selectedQuestion, string text)
        {
            selectedQuestion.CreateNewAnswer(text);
        }

        public bool RemoveSelectedQuiz()
        {
            return _quizGame.RemoveQuiz(_selectedQuiz);
        }
    }
}
