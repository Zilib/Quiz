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
        public bool IsSelected { get; private set; } = false;

        public Answer(Question question, string text, bool isCorrect)
        {
            _question = question;
            Text = text;
            IsCorrect = isCorrect;
        }

        public void SelectThisAnswer()
        {
            if (!_question.ExistSelectedAnswer())
            {
                throw new System.Exception("You cannot select two answers");
            }
            IsSelected = true;
        }
    }
}
