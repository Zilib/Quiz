using System;
using System.Collections.Generic;
using QuizApp.Services.Question;

namespace QuizApp.Model
{
    public class Question : AnswerService
    {
        protected override GameConfiguration _gameConfiguration { get; }
        private string title;
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

        public override List<Answer> Answers { get; protected set; } = new List<Answer>();

        public Question(string title, GameConfiguration gameConfiguration)
        {
            Title = title;
            _gameConfiguration = gameConfiguration;
        }
    }
}
