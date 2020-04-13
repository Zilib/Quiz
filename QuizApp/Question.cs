using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    [Serializable]
    public sealed class Question
    {
        #region Private variables

        private int order;

        private Answer[] answers = new Answer[Game.numberOfAnswers];

        #endregion

        #region Props

        public string Title { get; set; }
        public Answer[] Answers { get => answers; }

        #endregion

        #region Public lambda methods

        public bool ExistEmptyAnswer() => (from a in answers where a.Title == String.Empty select a).Count() != 0;
        public bool ExistOneCorrectAnswer() => (from a in answers where a.IsCorrect == true select a).Count() == 1;
        public int questionHasAnswer() => (from a in answers where a.IsSelected == true select a).Count();

        #endregion

        #region Constructors

        public Question() { }

        public Question(string title, int _order)
        {
            Title = title;
            order = _order; // Keep it in mind, it should be ever in this same order.
            //CreateAnswers();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Create 4 answers
        /// </summary>
        private void CreateAnswers()
        {
            for (int i = 0; i < Game.numberOfAnswers; i++)
            {
                Console.Clear();
                Console.WriteLine($"Text of answer number {i + 1}");
                string tempAnswer = Console.ReadLine();

                Validators.ValidString(tempAnswer, 6, "Input text of answer again");
                answers[i] = new Answer(tempAnswer);
            }

            Console.Clear();

            for (int i = 0; i < answers.Length; i++)
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
                || intInput > answers.Length - 1)
            {
                Console.Clear();
                Console.WriteLine("Sorry... Incorrect input, select correct answer again");

                for (int i = 0; i < answers.Length; i++)
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
            if (_answers == null 
                || (from a in _answers where a == null select a).Count() != 0)
                throw new System.ArgumentException("The answer's array cannot be null or empty");
            if ((from a in _answers where a.Title != string.Empty select a).Count() != Game.numberOfAnswers)
                throw new System.ArgumentException("Title of answers are incorrect!");
            if ((from a in _answers where a.IsCorrect == true select a).Count() != 1)
                throw new System.ArgumentException("At least one answer must be correct!");
            if (_answers.Length != Game.numberOfAnswers)
                throw new ArgumentException($"Number of answers is incorrect, correct number of answers is: {Game.numberOfAnswers}");

            for (int i = 0; i < answers.Length; i++)
                answers[i] = (Answer)_answers[i].Clone(); // It must be cloned, because i do not want to assing a refference
            
        }

        public bool SelectAndCheckAnswer(int answerIndex)
        {
            this.answers[answerIndex].IsSelected = true;
            return answers[answerIndex].IsSelected == answers[answerIndex].IsCorrect;
        }

        #endregion
    }

    [Serializable]
    public sealed class Answer : ICloneable
    {
        #region Constructor

        public Answer() {}
        public Answer(string title)
        {
            Title = title;
        }

        #endregion

        [NonSerialized]
        private bool isSelected = false;

        #region Public props

        public bool IsCorrect { get; set; } = false;
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public string Title { get; set; }

        #endregion

        public object Clone() => this.MemberwiseClone();
    }


}
