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
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException();
            }
            if (Questions.Count == _gameConfiguration.maxQuestions)
            {
                throw new ArgumentOutOfRangeException();
            }

            var newQuestion = new Question(title, _gameConfiguration);
            Questions.Add(newQuestion);
            return newQuestion;
        }

        public void SetAllDefault()
        {
            Questions.ForEach(x => x.SetAllAnswersDefault());
        }
    }
}
