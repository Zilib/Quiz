using System;


namespace QuizApp.Exceptions
{
    class IncorrectInputException : Exception
    {
        string Msg { get; }
        public IncorrectInputException(string msg)
        {
            Msg = msg;
        }
    }
}
