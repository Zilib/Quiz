using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        private readonly Game _game;
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

        public Question CreateNewQuestion(string title)
        {
            if (title.Length == 0 || title == null)
            {
                throw new System.Exception("Question title cannot be empty");
            }
            if (Questions.Count == _game.gameConfiguration.maxQuestions)
            {
                throw new System.Exception("You cannot create more questions!");
            }

            var questionToAdd = new Question(this, title, _game);
            Questions.Add(questionToAdd);

            return questionToAdd;
        }
    }
}
