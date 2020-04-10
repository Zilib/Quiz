using System;

namespace QuizApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game myQuizGame = new Game();
            Console.Clear();

            while (true)
            {
                Greetings(out string input);

                switch (input)
                {
                    case "1":
                        myQuizGame.CreateNewQuiz();
                        break;
                    case "2":
                        myQuizGame.SelectQuiz();
                        break;
                    case "3":
                        try
                        {
                            myQuizGame.CreateQuizTest();
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Tell greetings to user! Let him make choose what he want to do
        /// </summary>
        static void Greetings(out string input)
        {
            Console.WriteLine("***** Quiz Application ******\n");
            Console.WriteLine("What would you like to do?");
            Console.Write("Take [1] for create new quiz, [2] for take part of another one, [3] for create an exaple of quiz: ");

            input = Console.ReadLine();
            // if input is not 1 or 2, ask for another input
            while (input != "1" && input != "2" && input != "3")
            {
                Console.Clear();
                Console.WriteLine("Wrong input, please make your choose again!");
                Console.Write("Take [1] for create new quiz, [2] for take part of another one: ");
                input = Console.ReadLine();
            }
        }

        
    }
}
