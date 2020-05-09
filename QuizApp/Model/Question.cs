using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public sealed class Question
    {
        private string title;
        private readonly GameConfiguration _gameConfiguration;
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
            _gameConfiguration = gameConfiguration;
        }

        public Answer CreateNewAnswer(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException();
            }
            if (Answers.Count == _gameConfiguration.numberOfAnswers)
            {
                throw new IncorrectInputException("You cannot add anymore answers!");
            }
            Answer newAnswer = new Answer(text);
            Answers.Add(newAnswer);

            return newAnswer;
        }

        public void SelectAnswer(Answer answer)
        {
            if (!Answers.Contains(answer))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (Answers.Count != _gameConfiguration.numberOfAnswers)
            {
                throw new Exception("The number of answers is not equal to number of answers in configuration class.");
            }
            if (ExistOneSelectedAnswer())
            {
                throw new Exception("You cannot select twice");
            }
            answer.IsSelected = true;
        }

        public bool ExistOneSelectedAnswer()
        {
            return Answers.Select(x => x).Where(x => x.IsSelected).Count() == 1;
        }

        public bool ExistOneCorrectAnswer()
        {
            return Answers.Select(x => x).Where(x => x.IsCorrect).Count() == 1;
        }

        public void SetCorrectAnswer(Answer correctAnswer)
        {
            if (correctAnswer.IsCorrect)
            {
                throw new Exception("Answer is correct already");
            }
            if (!Answers.Contains(correctAnswer) || ExistOneCorrectAnswer())
            {
                throw new ArgumentOutOfRangeException();
            }
            correctAnswer.IsCorrect = true;
        }

        public void SetAllAnswersDefault()
        {
            Answers.Where(x => x.IsSelected).ToList().ForEach(x => x.IsSelected = false);
        }
    }
}
