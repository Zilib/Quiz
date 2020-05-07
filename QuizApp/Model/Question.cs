using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public sealed class Question
    {
        private readonly GameConfiguration _gameConfiguration;

        public string Title { get; set; }

        public List<Answer> Answers { get; private set; } = new List<Answer>();

        public Question(string title, GameConfiguration gameConfiguration)
        {
            Title = title;
            _gameConfiguration = gameConfiguration;
        }

        public bool ExistCorrectAnswer()
        {
            return Answers.Select(x => x).Where(x => x.IsCorrect == true).Count() == 1;
        }

        public bool ExistSelectedAnswer()
        {
            return Answers.Select(x => x).Where(x => x.IsSelected == true).Count() == 1;
        }

        public Answer CreateNewAnswer(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new IncorrectInputException("Answer text cannot be null");
            }
            if (Answers.Count == _gameConfiguration.numberOfAnswers)
            {
                throw new IncorrectInputException("You cannot add anymore answers!");
            }
            Answer answerToAdd = new Answer(text);
            Answers.Add(answerToAdd);

            return answerToAdd;
        }

        public void SetAllAnswersDefault()
        {
            Answers.Where(x => x.IsSelected == true).ToList().ForEach(x => x.UnSelectAnswer());
        }

        public void SelectCorrectAnswer(Answer correctAnswer)
        {
            if (Answers.Contains(correctAnswer))
            {
                correctAnswer.IsCorrect = true;
            }
        }

        public void SelectAnswer(Answer answer)
        {
            if (!Answers.Contains(answer))
            {
                Console.WriteLine("Incorrect answer");
                Console.ReadLine();
                return;
            }
            if (ExistSelectedAnswer())
            {
                Console.WriteLine("You cannot select twice");
                Console.ReadLine();
                return;
            }
            answer.IsSelected = true;
        }
    }
}
