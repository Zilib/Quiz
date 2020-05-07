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
        }

        public GameFascade(GameConfiguration gameConfiguration)
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

        public bool RemoveSelectedQuiz(Quiz quizToRemove)
        {
            return _quizGame.RemoveQuiz(quizToRemove);
        }
    }
}
