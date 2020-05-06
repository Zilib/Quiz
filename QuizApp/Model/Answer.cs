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
        private readonly Question _question;
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; private set; } = false;

        public Answer(Question question, string text)
        {
            _question = question;
            Text = text;
        }

        public bool IsUserAnswerCorrect()
        {
            return IsCorrect == IsSelected;
        }
        public override string ToString()
        {
            return Text;
        }
        public void SelectThisAnswer()
        {
            if (_question.ExistSelectedAnswer())
            {
                throw new IncorrectInputException("Answer is already selected!");
            }
            IsSelected = true;
        }
        public void UnSelectThisAnswer()
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
    }
}
