using System;

namespace QuizApp.Model
{
    public class Answer
    {
        public string Text { get; private set; }
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; } 

        public Answer(string text)
        {
            Text = text;
        }

        public EAnswerState? GetState()
        {
            if (IsCorrect && IsSelected)
            {
                return EAnswerState.Correct;
            }
            else if (IsCorrect == false && IsSelected)
            {
                return EAnswerState.Incorrect;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
