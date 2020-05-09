using System;

namespace QuizApp.Model
{
    public class Answer
    {
        private string text;
        public string Text 
        { 
            get => text;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException();
                }
                text = value;
            }
        }
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
            else if (!IsCorrect && IsSelected)
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
