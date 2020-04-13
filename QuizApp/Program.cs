using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;

namespace QuizApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game myQuizGame = new Game();

            while (true)
            {
                Greetings(out string input, new List<string> { "1","2","q","3" });

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
                            Console.ReadLine();
                        }
                        break;
                    case "q":
                        myQuizGame.SaveGame();
                        Console.ReadLine();
                        return;

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Tell greetings to user! Let him make choose what he want to do
        /// </summary>
        static void Greetings(out string input, List<String> validInputs)
        {
            Console.WriteLine("***** Aplikacja do Quizów ******\n");
            Console.WriteLine("Co chciałbyś zrobić?");
            Console.WriteLine("Wybierz [1] aby stworzyć nowy quiz, [2] aby wziąć udział w innym quizie, albo \"q\" aby wyjść i zapisać stworzone quizy: ");

            input = Console.ReadLine();
            // if input is not 1 or 2, ask for another input
            while (!validInputs.Contains(input))
            {
                Console.Clear();
                Console.WriteLine("Zły znak, wprowadź znak ponownie!");
                Console.Write(""Wybierz[1] aby stworzyć nowy quiz, [2] aby wziąć udział w innym quizie, albo \"q\" aby wyjść i zapisać stworzone quizy:");
                input = Console.ReadLine();
            }
        }
    }
}
