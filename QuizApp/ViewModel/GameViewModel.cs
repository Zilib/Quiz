using QuizApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Fascade
{
    public partial class GameViewModel
    {
        private readonly Game _quizGame = null;
        public List<string> Errors { get; private set; }

        public GameViewModel(int numberOfAnswers, int minQuestions, int maxQuestions, int minTitleLength, string saveFileName)
        {
            Errors = new List<string>();
            _quizGame = new Game(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
        }

        public GameViewModel(GameConfiguration gameConfiguration)
        {
            Errors = new List<string>();
            _quizGame = new Game(gameConfiguration);
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }

        public Quiz CreateNewQuiz(string title)
        {
            return _quizGame.CreateNewQuiz(title);
        }

        public Question CreateNewQuestion(Quiz currentQuiz, string questionTitle)
        {
            return currentQuiz.CreateNewQuestion(questionTitle);
        }

        public Answer CreateNewAnswer(Question selectedQuestion, string text)
        {
            return selectedQuestion.CreateNewAnswer(text);
        }

        public List<Quiz> GetQuizes(bool setAllDefault)
        {
            return _quizGame.GetQuizes(setAllDefault);
        }

        public bool RemoveSelectedQuiz(Quiz quizToRemove)
        {
            return _quizGame.RemoveQuiz(quizToRemove);
        }
    }
}
