using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    partial class Game
    {
        public void CreateNewQuiz()
        {
            Console.Clear();
            Quiz tempQuiz = new Quiz();

            #region Set quiz main data

            Console.WriteLine("Wprowadź tytuł quizu!");
            tempQuiz.Title = Console.ReadLine();
            Validators.ValidString(tempQuiz.Title, 4, "Wprowadź tytuł quizu!");

            Console.Clear();
            Console.WriteLine("Wprowadź opis quizu");
            tempQuiz.Description = Console.ReadLine();
            Validators.ValidString(tempQuiz.Description, 20, "Wprowadź opis quizu");

            #endregion

            #region Create Questions

            Console.Clear();
            Console.WriteLine($"Ile pytań quiz ma zawierać? ( Więcej niż {minQuestions} i mniej niż {maxQuestions})");

            string input = Console.ReadLine();
            // Untill input is not int or is larger than 10 or smaller than 2
            int numberOfQuestions;
            while (!Int32.TryParse(input, out numberOfQuestions)
                || (numberOfQuestions <= 2
                || numberOfQuestions >= 10))
            {
                Console.Clear();
                Console.WriteLine("Wprowadzono błędne dane!!");
                Console.WriteLine($"Ile pytań quiz ma zawierać? ( Więcej niż {minQuestions} i mniej niż ${maxQuestions})");
                input = Console.ReadLine();
            }

            for (int i = 0; i < numberOfQuestions; i++)
            {
                tempQuiz.CreateQuestion();
            }

            #endregion

            Quizes.Add(tempQuiz);
        }

        private void ShowQuizes()
        {
            for (int i = 0; i < Quizes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {Quizes[i].Title}");
            }
        }
    }
}
