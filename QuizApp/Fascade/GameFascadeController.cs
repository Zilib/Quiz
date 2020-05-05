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
            quizGame = new Game(numberOfAnswers, minQuestions, maxQuestions, minTitleLength, saveFileName);
        }

        public GameFascade(GameConfiguration gameConfiguration)
        {
            Errors = new List<string>();
            quizGame = new Game(gameConfiguration);
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }

        public Quiz CreateNewQuiz(string title)
        {
            if (title.Length < quizGame.gameConfiguration.minTitleLength)
            {
                throw new IncorrectInputException("Quiz title is not long enought.");
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
                throw new QuizIsNotSelectedException();
            }
        }

        public Question CreateNewQuestion(Quiz selectedQuiz, string questionTitle)
        {
            if (!GetQuizes().Contains(selectedQuiz))
            {
                throw new ArgumentOutOfRangeException();
            }

            this.SelectedQuiz = selectedQuiz;
            var newQuestion = this.SelectedQuiz.CreateNewQuestion(questionTitle);

            selectedQuestion = newQuestion;
            return newQuestion;
        }

        public Question CreateNewQuestion(string questionTitle)
        {
            if (SelectedQuiz == null)
            {
                throw new QuizIsNotSelectedException();
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
