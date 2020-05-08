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
            GameConfiguration gameConfiguration = new GameConfiguration(4, 1, 4, 4, "test.txt");
            GameViewModel game = new GameViewModel(gameConfiguration);
            QuizTestExample test = new QuizTestExample(game);
            test.CreateTestQuiz();

            Menu(game, gameConfiguration);
        }

        private static void Menu(GameViewModel fascade, GameConfiguration gameConfiguration)
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
                if (!MenuSelect(Console.ReadLine().ToLower() ?? "0", fascade, gameConfiguration))
                {
                    break;
                }
                Console.WriteLine();
            }
        }

        private static bool MenuSelect(string answer, GameViewModel fascade, GameConfiguration gameConfiguration)
        {
            GameView view = new GameView(fascade);

            QuizBuilder quizBuilder = new QuizBuilder(fascade, gameConfiguration);
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
                        quizBuilder.BuildQuiz();
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