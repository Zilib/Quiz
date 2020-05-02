using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model
{
    public sealed class Answer
    {
        public int QuestionId { get; private set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Answer(int questionId, string text, bool isCorrect)
        {
            QuestionId = questionId;
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}
