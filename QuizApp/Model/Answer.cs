using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Model
{
    public sealed class Answer
    {
        public string Text { get; private set; }
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; } 

        public Answer(string text)
        {
            Text = text;
        }

        public bool IsUserAnswerCorrect()
        {
            return IsCorrect == IsSelected;
        }

        public void SetAnswerCorrect()
        {

        }

        public void UnSelectAnswer()
        {
            IsSelected = false;
        }

        public EAnswerState GetState()
        {
            if (IsCorrect == true && IsSelected == true)
            {
                return EAnswerState.Correct;
            }
            else if (IsCorrect == false && IsSelected == true)
            {
                return EAnswerState.Incorrect;
            }
            else
            {
                return EAnswerState.Normal;
            }
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
