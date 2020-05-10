using QuizApp.Exceptions;
using QuizApp.Services.Question;
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
            if (Questions.Count == _gameConfiguration.maxQuestions)
            {
                throw new Exception("You cannot add anymore questions!");
            }
            if (!questionToInsert.IsGameConfigurationRight(_gameConfiguration))
            {
                throw new Exception("Question is using wrong game configuration!!");
            }
            Questions.Add(questionToInsert);
        }

        public void SetAllDefault()
        {
            Questions.ForEach(x => x.SetAllAnswersDefault());
        }
    }
}
