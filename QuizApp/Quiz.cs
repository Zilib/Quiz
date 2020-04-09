using System;
using System.Collections;
using System.Collections.Generic;

namespace QuizApp
{
    class Game
    {
        #region Private variables

        // Number of all quizes
        private List<Quiz> Quizes = new List<Quiz>();

        #endregion

        #region Public lambda methods 

        public int GetNumberOfQuizes => Quizes.Count;

        #endregion

        #region Public methods

        public void CreateNewQuiz()
        {
            Console.Clear();
            Quiz tempQuiz = new Quiz();

            #region Set quiz main data

            Console.WriteLine("Please input quiz title");
            tempQuiz.Title = Console.ReadLine();
            Validators.ValidString(tempQuiz.Title, 4, "Please input quiz title");

            Console.Clear();
            Console.WriteLine("Please input quiz description");
            tempQuiz.Description = Console.ReadLine();
            Validators.ValidString(tempQuiz.Description, 20, "Please input quiz description");

            #endregion

            #region Create Questions

            Console.Clear();
            Console.WriteLine("How many questions do you want? ( More than 2 and less than 10)");

            string input = Console.ReadLine();
            // Untill input is not int or is larger than 10 or smaller than 2
            int numberOfQuestions;
            while(!Int32.TryParse(input, out numberOfQuestions) 
                || (numberOfQuestions <= 2 
                || numberOfQuestions >= 10))
            {
                Console.Clear();
                Console.WriteLine("Wrong input!");
                Console.WriteLine("How many questions do you want? ( More than 2 and less than 10)");
                input = Console.ReadLine();
            }

            for(int i = 0; i < numberOfQuestions; i++)
            {
                tempQuiz.CreateQuestion();
            }

            #endregion

            Quizes.Add(tempQuiz);
        }

        public void SelectQuiz()
        {
            Console.Clear();

            if (Quizes.Count == 0)
            {
                Console.WriteLine("No created quiz!");
                return;
            }

            Console.WriteLine("Select your quiz! ");
            ShowQuizes();

            string input = Console.ReadLine();
            int intInput;
            while (!Int32.TryParse(input,out intInput)
                || intInput < 0
                || intInput > Quizes.Count)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input! Please choose quiz again!");
                ShowQuizes();
                input = Console.ReadLine();
            }


            Console.WriteLine();
        }

        private void ShowQuizes()
        {
            for (int i = 0; i < Quizes.Count; i++)
            {
                Console.WriteLine($"[{i}]. {Quizes[i].Title}");
            }
        }

        #endregion

    }

    sealed class Quiz
    {
        private List<Question> Questions { get; set; } = new List<Question>();
        public string Title { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Fill basic question data, and make 4 available answers for it
        /// </summary>
        public void CreateQuestion()
        {
            Console.Clear();
            Console.WriteLine("Give your question text:");

            string tempText = Console.ReadLine();
            Validators.ValidString(tempText, 10, "Give your text:");

            Questions.Add(new Question(tempText, Questions.Count));
        }
    }

}