using QuizApp.Exceptions;
using System;
using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        private string title;
        public string Title 
        {
            get => title;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                if (value.Length < _gameConfiguration.minTitleLength)
                {
                    throw new Exception("Title is too short");
                }
                title = value;
            }
        }
        public List<Question> Questions { get; private set; } = new List<Question>();
        private readonly GameConfiguration _gameConfiguration;

        public Quiz(string title, GameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            Title = title;
        }

        public Question CreateNewQuestion(string title)
        {
            if (Questions.Count == _gameConfiguration.maxQuestions)
            {
                throw new Exception("You cannot add anymore questions!");
            }

            var newQuestion = new Question(title, _gameConfiguration);
            Questions.Add(newQuestion);
            return newQuestion;
        }

        public void InsertNewQuestion(Question questionToInsert)
        {
            if (!_gameConfiguration.Equals(questionToInsert.GameConfiguration))
            {
                throw new Exception("Invalid question game configuration");
            }
            if (Questions.Count == _gameConfiguration.maxQuestions)
            {
                throw new Exception("You cannot add anymore questions!");
            }
            Questions.Add(questionToInsert);
        }

        public void RemoveQuestion(Question questionToRemove)
        {
            if (!Questions.Contains(questionToRemove))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public void SetAllDefault()
        {
            Questions.ForEach(x => x.SetAllAnswersDefault());
        }
    }
}
