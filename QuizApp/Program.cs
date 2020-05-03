using QuizApp.Model;
using System;
using System.Collections.Generic;

namespace QuizApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GameFascade game = new GameFascade(4, 1, 4, 4, "test.txt");

            Menu(game);
        }


        private static void Menu(GameFascade fascade)
        {
            while(true)
            {
                Console.Clear();

                if (fascade.Errors.Count != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;

                    var errors = fascade.Errors;
                    errors.ForEach(x =>
                    {
                        Console.WriteLine(x);
                    });
                    fascade.ClearErrors();

                    Console.ResetColor();
                    Console.WriteLine();
                }

                Console.WriteLine("Hello tell me what would you like to do:");
                Console.WriteLine("1. Give answer for existing quiz");
                Console.WriteLine("2. Create your own quiz");
                Console.WriteLine("q. Exit game");
                if (!MenuSelect(Console.ReadLine().ToLower() ?? "0", fascade))
                {
                    break;
                }
                Console.WriteLine();
            }
        }

        private static bool MenuSelect(string answer, GameFascade fascade)
        {
            QuizBuilder quizBuilder = new QuizBuilder(fascade);
            switch (answer)
            {
                case "1":
                    ShowQuizesList(fascade.GetQuizes());
                    break;
                case "2":
                    try
                    {
                    quizBuilder.BuildQuiz()
                        .BuildQuestions()
                        .Build();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    break;
                case "q":
                    return false;
                    break;
            }
            return true;
        }

        private static void ShowQuizesList(List<Quiz> quizes)
        {
            for (int i = 0; i < quizes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {quizes[i].Title}");
            }
            Console.ReadLine();
        }

        private static void CreateNewQuiz(GameFascade fascade)
        {
            string quizTitle = Console.ReadLine();
            fascade.CreateNewQuiz(quizTitle);
            return;
        }
    }
}