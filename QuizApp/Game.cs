using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class Game
    {
        public static bool testsAvailable { get; } = true;
        #region Private variables

        // Number of all quizes
        private List<Quiz> Quizes = new List<Quiz>();

        #endregion

        #region Private methods

        private void CreateNewQuiz(string title, string description, Question[] questions)
        {
            Quiz tempQuiz = new Quiz();

            #region Validate arguments, and set quiz main data

            // Validate questions array
            if (questions == null)
                throw new System.ArgumentException("Qusetions are null");
            if (questions.Length == 0)
                throw new System.ArgumentException("Questions are empty");

            if (title == string.Empty)
                throw new System.ArgumentException("Title cannot be empty!");
            if (title.Length < Quiz.minTitleLength)
                throw new System.ArgumentException("Title is too short!");
            tempQuiz.Title = title;

            if (description == string.Empty)
                throw new System.ArgumentException("Description cannot be empty!");
            if (description.Length < Quiz.minDescriptionLength)
                throw new System.ArgumentException("Description is too short!");
            tempQuiz.Description = description;

            #endregion

            try
            {
                tempQuiz.SetQuestions(questions.ToList());
            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Quizes.Add(tempQuiz);
        }

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
            while (!Int32.TryParse(input, out numberOfQuestions)
                || (numberOfQuestions <= 2
                || numberOfQuestions >= 10))
            {
                Console.Clear();
                Console.WriteLine("Wrong input!");
                Console.WriteLine("How many questions do you want? ( More than 2 and less than 10)");
                input = Console.ReadLine();
            }

            for (int i = 0; i < numberOfQuestions; i++)
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
            while (!Int32.TryParse(input, out intInput)
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

        public void CreateQuizTest()
        {
            if (!Game.testsAvailable)
                throw new System.ArgumentException("Sorry but this metod is unable to use");
            Queue<string> titles = new Queue<string>();
            titles.Enqueue("Pierwsze pytanie");
            titles.Enqueue("Drugie pytanie");
            titles.Enqueue("Trzecie pytanie");
            titles.Enqueue("Czwarte pytanie");
            titles.Enqueue("Piąte pytanie");
            titles.Enqueue("Szóste pytanie");
            titles.Enqueue("Siódme pytanie");
            titles.Enqueue("Dziewiąte pytanie");
            titles.Enqueue("Dziesiąte pytanie");
            titles.Enqueue("Jedynaste pytanie");
            titles.Enqueue("Dwunaste pytanie");

            List<Answer> answersText = new List<Answer>()
            {
                new Answer("Pierwsze pytanie"),
                new Answer("Drugie pytanie"),
                new Answer("Trzecie pytanie"),
                new Answer("Czwarte pytanie"),
            };

            answersText[2].isCorrect = true;

            Random correctAnswer = new Random();

            Question[] questions = new Question[10];
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i] = new Question(titles.Peek(), i);
                titles.Dequeue();
                // Make random answer correct answer
                try
                {
                    questions[i].SetAnswers(answersText);
                }
               catch (System.ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                
            }
            CreateNewQuiz("Mój przykładowy quiz", "Mój pierwszy quiz!", questions);
        }

    }
}
