using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public sealed class Question
    {
        private readonly Quiz _quiz;
        private readonly Game _quizGame;

        public string Title { get; set; }

        public List<Answer> answers { get; private set; } = new List<Answer>();

        public Question(Quiz quiz, string title, Game game)
        {
            _quizGame = game;
            Title = title;
            _quiz = quiz;
        }

        public Quiz GetMyQuiz()
        {
            return _quiz;
        }

        public List<Answer> GetAnswers()
        {
            if (answers.Count != _quizGame.gameConfiguration.numberOfAnswers)
            {
                throw new ArgumentOutOfRangeException("Amount of answers is incorrect, amount of answers is: " + _quizGame.gameConfiguration.numberOfAnswers);
            }

            return answers;
        }

        public Answer GetAnswer(int answerIndex)
        {
            return answers[answerIndex];
        }

        public bool ExistCorrectAnswer()
        {
            return answers.Select(x => x).Where(x => x.IsCorrect == true).Count() == 1;
        }

        public bool ExistSelectedAnswer()
        {
            return answers.Select(x => x).Where(x => x.IsSelected == true).Count() == 1;
        }

        public Answer CreateNewAnswer(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new IncorrectInputException("Answer text cannot be null");
            }
            if (answers.Count == _quizGame.gameConfiguration.numberOfAnswers)
            {
                throw new IncorrectInputException("You cannot add anymore answers!");
            }
            Answer answerToAdd = new Answer(this, text);
            answers.Add(answerToAdd);

            return answerToAdd;
        }

        public void SetAllAnswersDefault()
        {
            answers.Where(x => x.IsSelected == true).ToList().ForEach(x => x.UnSelectThisAnswer());
        }

        public void SelectCorrectAnswer(Answer correctAnswer)
        {
            if (answers.Contains(correctAnswer))
            {
                correctAnswer.IsCorrect = true;
            }
        }

        public void SelectAnswer(int answerIndex)
        {
            if (ExistSelectedAnswer())
            {
                Console.WriteLine("You cannot select twice");
                Console.ReadLine();
                return;
            }
            answers[answerIndex].SelectThisAnswer();
        }
    }
}
