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

            Console.Clear();
            Console.WriteLine("How many questions do you want? ( More than 2 and less than 10)");

            string input = Console.ReadLine();
            while(!Int32.TryParse(input, out int numberOfQuestions) 
                || (numberOfQuestions <= 2 
                || numberOfQuestions >= 10))
            {
                Console.Clear();
                Console.WriteLine("Wrong input!");
                Console.WriteLine("How many questions do you want? ( More than 2 and less than 10)");
                input = Console.ReadLine();
            }


        }

        #endregion

    }

    sealed class Quiz
    {
        private List<Question> Questions { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public void CreateQuestion()
        {
            Console.Clear();
            Console.WriteLine("Give your question text:");

            string tempText = Console.ReadLine();
            Validators.ValidString(tempText, 10, "Give your text:");

            Questions.Add(new Question(tempText));
        }
    }

    /// <summary>
    /// Every important things for quiz texts
    /// </summary>
    abstract class FieldVariables
    {
        public FieldVariables(string title)
        {
            Text = title;
        }

        public string Text { get; set; }
    }

    sealed class Question : FieldVariables
    {
        private List<Answer> answers;

        public Question(string title)
            : base(title)
        {
            CreateAnswers();
        }

        /// <summary>
        /// Create 4 answers
        /// </summary>
        private void CreateAnswers()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.Clear();
            }
        }
    }

    sealed class Answer : FieldVariables
    {
        public Answer(string title)
            : base (title)
        {

        }
        public bool IsCorrect { get; set; }
    }

    static class Validators
    {
        /// <summary>
        /// Wait for user input untill he's input does not meet the requirements
        /// </summary>
        /// <param name="input">Property</param>
        /// <param name="minLength"></param>
        /// <param name="msg">Request for new input</param>
        public static void ValidString(string input, int minLength, string msg)
        {
            while (input.Length <= 3)
            {
                Console.Clear();
                Console.WriteLine($"Input is too short, minimum length is {minLength}");
                Console.WriteLine(msg);
                input = Console.ReadLine();
            }
        }
    }
}
