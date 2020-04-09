using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            while (input.Length <= 3)
            {
                Console.Clear();
                Console.WriteLine($"Input is too short, minimum length is {minLength}");
                Console.WriteLine(msg);
                input = Console.ReadLine();
            }
        }
    }
}
