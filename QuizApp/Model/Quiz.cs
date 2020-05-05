﻿using QuizApp.Exceptions;
using System;
using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        private readonly Game _game;
        private Question selectedQuestion;

        public string Title { get; set; }

        public List<Question> Questions { get; private set; }

        public bool CanBeSelected(Game game) => game == _game;

        public Quiz(string title, Game game)
        {
            _game = game;
            Title = title;
            Questions = new List<Question>();
        }

        public Question GetQuestion(int questionIndex)
        {
            return Questions[questionIndex];
        }

        public Question GetSelectedQuestion() => selectedQuestion;

        public void CreateNewQuestion(string title)
        {
            if (title.Length == 0 || title == null)
            {
                throw new NullReferenceException();
            }
            if (Questions != null && Questions.Count + 1 == _game.gameConfiguration.maxQuestions)
            {
                throw new IncorrectInputException("You cannot create more questions!");
            }

            var questionToAdd = new Question(this, title, _game);
            Questions.Add(questionToAdd);
            selectedQuestion = questionToAdd;


        }
    }
}
