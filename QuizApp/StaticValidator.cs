using System;


namespace QuizApp
{
    static class Validators
    {
        /// <summary>
        /// Wait for user input untill he's input does not meet the requirements
        /// </summary>
        /// <param name="input">Property</param>
        /// <param name="minLength"></param>
        /// <param name="msg">Request for new input</param>
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
