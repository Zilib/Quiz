using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        public string Title { get; set; }

        private List<Question> questions;

        public Quiz(string title)
        {
            Title = title;
            questions = new List<Question>();
        }

        public Question GetQuestion(int questionIndex)
        {
            return questions[questionIndex];
        }

        public Question CreateNewQuestion(string title)
        {
            if (title.Length == 0 || title == null)
            {
                throw new System.Exception("Question title cannot be empty");
            }
            var questionToAdd = new Question(this, title);
            questions.Add(questionToAdd);

            return questionToAdd;
        }
    }
}
