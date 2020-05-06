using QuizApp.Exceptions;
using System;
using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        public string Title { get; set; }
        private List<Question> questions;

        private readonly Game _game;
        private Question selectedQuestion;

        public Quiz(string title, Game game)
        {
            _game = game;
            Title = title;
            questions = new List<Question>();
        }
        public Question GetQuestion(int questionIndex)
        {
            return questions[questionIndex];
        }
        public List<Question> GetQuestions()
        {
            return questions;
        }
        public Question GetSelectedQuestion()
        {
            return selectedQuestion;
        }
        public void CreateNewQuestion(string title)
        {
            if (title.Length == 0 || title == null)
            {
                throw new NullReferenceException();
            }
            if (questions != null && questions.Count + 1 == _game.gameConfiguration.maxQuestions)
            {
                throw new IncorrectInputException("You cannot create more questions!");
            }

            var questionToAdd = new Question(this, title, _game);
            questions.Add(questionToAdd);
            selectedQuestion = questionToAdd;


        }
        public bool CanBeSelected(Game game)
        {
         return game == _game;
        }
    }
}
