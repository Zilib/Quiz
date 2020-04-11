using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    sealed class Question
    {
        #region Public static

        public static int numberOfAnswers { get; } = 4;

        #endregion

        public List<Answer> answers { get; set; } = new List<Answer>();

        #region Private variables

        private int order;

        #endregion

        #region Props

        public bool IsFilled { get; set; } = false;
        public string Title { get; set; }


        #endregion

        #region Public lambda methods

        /// <summary>
        /// Return false if answers are not equal to numberOfAnswers
        /// </summary>
        public bool isAnswersFilled() => answers.Count == numberOfAnswers;
        public bool existOneCorrectAnswer() => (from a in answers where a.IsCorrect == true select a).Count() == 1;
        public int questionHasAnswer => (from a in answers where a.IsSelected == true select a).Count(); 

        #endregion

        #region Constructors

        public Question(string title, int _order)
        {
            Title = title;
            order = _order; // Keep it in mind, it should be ever in this same order.
            //CreateAnswers();
        }

        public Question() { }

        #endregion

        #region Private methods

        /// <summary>
        /// Create 4 answers
        /// </summary>
        private void CreateAnswers()
        {
            for (int i = 0; i < numberOfAnswers; i++)
            {
                Console.Clear();
                Console.WriteLine($"Text of answer number {i + 1}");
                string tempAnswer = Console.ReadLine();

                Validators.ValidString(tempAnswer, 6, "Input text of answer again");
                answers.Add(new Answer(tempAnswer));
            }

            Console.Clear();

            for (int i = 0; i < answers.Count; i++)
            {
                Console.Write($"[{i}]. {answers[i].Title}\n");
            }

            SetCorrectAnswer();
            Console.WriteLine();
        }

        /// <summary>
        /// Choose one correct answer from created answers
        /// </summary>
        private void SetCorrectAnswer()
        {
            Console.Write("\nOkey... You have to select now, which answer is correct: ");

            string input = Console.ReadLine();

            // Validate input
            int intInput;
            while (!Int32.TryParse(input, out intInput)
                || intInput < 0
                || intInput > answers.Count - 1)
            {
                Console.Clear();
                Console.WriteLine("Sorry... Incorrect input, select correct answer again");

                for (int i = 0; i < answers.Count; i++)
                {
                    Console.Write($"[{i}]. {answers[i].Title}\n");
                }

                input = Console.ReadLine();
            }

            answers[intInput].IsCorrect = true;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Available only whenever developer allow for it, it serve for instantly creating quiz
        /// </summary>
        /// <param name="_answers"></param>
        public void SetAnswers(Answer[] _answers)
        {
            if (!Game.testsAvailable)
                throw new System.ArgumentException("Sorry but this metod is unable to use");
            if (_answers == null)
                throw new System.ArgumentException("The answer's array cannot be null");
            if ((from a in _answers where a.Title != string.Empty select a).Count() != numberOfAnswers)
                throw new System.ArgumentException($"Number of answers are incorrect! Correct number of answers is: {numberOfAnswers}");
            if ((from a in _answers where a.IsCorrect == true select a).Count() != 1)
                throw new System.ArgumentException("At least one answer must be correct!");

            foreach (Answer a in _answers)
                answers.Add((Answer)a.Clone());
            
        }

        public bool SelectAndCheckAnswer(int answerIndex)
        {
            this.answers[answerIndex].IsSelected = true;
            return answers[answerIndex].IsSelected == answers[answerIndex].IsCorrect;
        }

        #endregion
    }

    sealed class Answer : ICloneable
    {
        #region Constructor

        public Answer() {}
        public Answer(string title)
        {
            Title = title;
        }

        #endregion

        #region Public props

        public bool IsCorrect { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public string Title { get; set; }

        public object Clone() => this.MemberwiseClone();
   

        #endregion
    }


}
