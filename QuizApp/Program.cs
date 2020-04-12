using System;
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
            LoadGame(ref myQuizGame);
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
                    case "q":
                        SaveGame(myQuizGame);
                        return;

                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Tell greetings to user! Let him make choose what he want to do
        /// </summary>
        static void Greetings(out string input)
        {
            Console.WriteLine("***** Aplikacja do Quizów ******\n");
            Console.WriteLine("Co chciałbyś zrobić?");
            Console.Write("Wybierz [1] aby stworzyć nowy quiz, [2] aby stworzyć inny quiz, albo \"q\" aby wyjść i zapisać stworzone quizy: ");

            input = Console.ReadLine();
            // if input is not 1 or 2, ask for another input
            while (input != "1" && input != "2" && input != "q")
            {
                Console.Clear();
                Console.WriteLine("Wrong input, please make your choose again!");
                Console.Write("Take [1] for create new quiz, [2] for take part of another one: ");
                input = Console.ReadLine();
            }
        }

        private static void SaveGame(Game quizToSave)
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream("quizes.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, quizToSave);
            }
        }

        private static void LoadGame(ref Game gameToLoad)
        {
            // If file doesn't exist don't load it
            if (!File.Exists("quizes.dat"))
                return;

            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = File.OpenRead("quizes.dat"))
            {
                gameToLoad = (Game)binFormat.Deserialize(fStream);
            }
        }
    }
}
