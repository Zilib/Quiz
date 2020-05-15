using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public sealed class Question
    {
        private string title;
        public GameConfiguration GameConfiguration { get; private set; }
        public string Title
        {
            get => title;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException();
                }
                title = value;
            }
        }

        public List<Answer> Answers { get; private set; } = new List<Answer>();

        public Question(string title, GameConfiguration gameConfiguration)
        {
            Title = title;
            GameConfiguration = gameConfiguration;
        }

        public Answer CreateNewAnswer(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException();
            }
            if (Answers.Count == GameConfiguration.numberOfAnswers)
            {
                throw new Exception();
            }
            Answer newAnswer = new Answer(text);
            Answers.Add(newAnswer);

            return newAnswer;
        }

        public void InsertNewAnswer(Answer answer)
        {
            if (!(GameConfiguration.numberOfAnswers < Answers.Count))
            {
                throw new Exception();
            }
            Answers.Add(answer);
        }
        
        public void SelectAnswer(Answer answer)
        {
            if (!Answers.Contains(answer))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (Answers.Count != GameConfiguration.numberOfAnswers)
            {
                throw new Exception("The number of answers is not equal to number of answers in configuration class.");
            }
            if (IsAnyAnswerSelected())
            {
                throw new Exception("You cannot select twice");
            }
            // todo if correct answer doesn't exist do not allow to select any answer
            answer.IsSelected = true;
        }

        public bool IsAnyAnswerSelected()
        {
            return Answers.Select(x => x).Where(x => x.IsSelected).Count() == 1;
        }

        public void SetCorrectAnswer(Answer correctAnswer)
        {
            if (correctAnswer.IsCorrect)
            {
                throw new Exception("Answer is correct already");
            }
            if (!Answers.Contains(correctAnswer) || IsOneAnswerCorrect())
            {
                throw new ArgumentOutOfRangeException();
            }
            correctAnswer.IsCorrect = true;
        }

        public void SetAllAnswersDefault()
        {
            Answers.Where(x => x.IsSelected).ToList().ForEach(x => x.IsSelected = false);
        }

        private bool IsOneAnswerCorrect()
        {
            return Answers.Select(x => x).Where(x => x.IsCorrect).Count() == 1;
        }
    }
}