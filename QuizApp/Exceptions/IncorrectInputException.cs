using System;


namespace QuizApp.Exceptions
{
    public class IncorrectInputException : Exception
    {
        string Msg { get; }
        public IncorrectInputException(string msg)
        {
            Msg = msg;
        }
    }
}
