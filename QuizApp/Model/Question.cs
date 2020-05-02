using System.Collections.Generic;

namespace QuizApp.Model
{
    public sealed class Question
    {
        private readonly Quiz _quizId;
        public string Title { get; set; }
        public List<Answer> Answers { get; set; }

        public Question(Quiz quiz)
        {
            _quizId = quiz;
        }
    }
}
