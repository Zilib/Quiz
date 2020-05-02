using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model
{
    public sealed class Answer
    {
        private readonly Question _question;
        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public Answer(Question question, string text, bool isCorrect)
        {
            _question = question;
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}
