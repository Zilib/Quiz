using System;

namespace QuizApp.Services
{
    public static class ConsoleWriter
    {
        public static void WriteInForegroundColor(string text, ConsoleColor? foregroundColor)
        {
            if (foregroundColor != null)
            {
                Console.ForegroundColor = (ConsoleColor)foregroundColor;
                Console.WriteLine(text);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}