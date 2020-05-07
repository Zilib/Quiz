using QuizApp.Exceptions;
using System;
using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        public string Title { get; set; }
        public List<Question> Questions { get; private set; } = new List<Question>();
        private readonly GameConfiguration _gameConfiguration;

        public Quiz(GameConfiguration gameConfiguration, string title)
        {
            _gameConfiguration = gameConfiguration;
            Title = title;
        }

        public Question CreateNewQuestion(string title)
        {
            if (title.Length == 0 || title == null)
            {
                throw new NullReferenceException();
            }
            if (Questions != null && Questions.Count + 1 == _gameConfiguration.maxQuestions)
            {
                throw new IncorrectInputException("You cannot create more questions!");
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
