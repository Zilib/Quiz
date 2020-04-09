using System;

namespace QuizApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Greetings();
            Console.ReadLine();
        }

        /// <summary>
        /// Tell greetings to user!
        /// </summary>
        static void Greetings()
        {
            Console.WriteLine("***** Quiz Application ******\n");
            Console.WriteLine("What would you like to do?");
            Console.Write("Take [1] for create new quiz, [2] for take part of another one: ");

            string input = Console.ReadLine();
            // if input is not 1 or 2, ask for another input
            while (input != "1" && input != "2")
            {
                Console.Clear();
                Console.WriteLine("Wrong input, please make your choose again!");
                Console.Write("Take [1] for create new quiz, [2] for take part of another one: ");
                input = Console.ReadLine();
            }
        }
    }
}
