using System;


namespace QuizApp
{
    static class Validators
    {
        public static void ValidString(string input, int minLength, string msg)
        {
            while (input.Length < minLength)
            {
                Console.Clear();
                Console.WriteLine($"Wprowadzone dane znakowe są za krótkie, minimalna długość to: {minLength}");
                Console.WriteLine(msg);
                input = Console.ReadLine();
            }
        }
    }
}
