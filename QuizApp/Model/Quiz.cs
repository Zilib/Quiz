using System.Collections.Generic;

namespace QuizApp.Model
{
    public class Quiz
    {
        public int QuizId { get; private set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz(int id, string title)
        {
            QuizId = id;
            Title = title;
        }
    }
}
