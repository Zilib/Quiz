using QuizApp.Fascade;
using QuizApp.Model;
using QuizApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;

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

                if (fascade.Errors.Any())
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
            GameView view = new GameView(fascade);

            QuizBuilder quizBuilder = new QuizBuilder(fascade);
            switch (answer)
            {
                case "1":
                    try
                    {
                        Console.Clear();
                        view.PlayQuiz();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case "2":
                    try
                    {
                        quizBuilder.BuildQuiz()
                            .BuildQuestions();
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
    }
}