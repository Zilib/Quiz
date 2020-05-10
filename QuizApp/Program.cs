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
            Game game = new Game(gameConfiguration);
            QuizTestExample test = new QuizTestExample(game);
            test.CreateTestQuiz();

            Menu(game);
        }

        private static void Menu(Game game)
        {
            while(true)
            {
                Console.Clear();

                Console.WriteLine("Hello tell me what would you like to do:");
                Console.WriteLine("1. Give answer for existing quiz");
                Console.WriteLine("2. Create your own quiz");
                Console.WriteLine("3. Remove quiz");
                Console.WriteLine("q. Exit game");
                if (!MenuSelect(Console.ReadLine().ToLower() ?? "0", game))
                {
                    break;
                }
                Console.WriteLine();
            }
        }

        private static bool MenuSelect(string answer, Game game)
        {
            GameView view = new GameView(game);

            QuizBuilder quizBuilder = new QuizBuilder(game);
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
                case "3":
                    try
                    {
                        view.RemoveQuiz();
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
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