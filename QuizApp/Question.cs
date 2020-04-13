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

        private int order = 0;

        #endregion

        #region Props

        public string Title { get; set; }
        public Answer[] Answers { get; private set; } = new Answer[Game.numberOfAnswers];

        #endregion

        #region Public lambda methods

        public bool ExistEmptyAnswer() => (from a in Answers where a.Title == String.Empty select a).Count() != 0;
        public bool ExistOneCorrectAnswer() => (from a in Answers where a.IsCorrect == true select a).Count() == 1;
        public int questionHasAnswer() => (from a in Answers where a.IsSelected == true select a).Count();

        #endregion

        #region Constructors

        public Question() { }

        public Question(string title, int _order)
        {
            Title = title;
            order = _order; // Keep it in mind, it should be ever in this same order.
            CreateAnswers();
        }

        /// <summary>
        /// If you want to add question from code use this! 
        /// </summary>
        /// <param name="title">Title of question</param>
        /// <param name="_order"></param>
        /// <param name="answers">An array with answers</param>
        public Question(string title, int _order, Answer[] answers)
        {
            #region Validate answers

            if ((from a in answers where a == null select a).Count() != 0)
                throw new System.ArgumentException("Tablica z odpowiedziami nie może być pusta!");
            if ((from a in answers where a.Title == String.Empty select a).Count() != 0)
                throw new System.ArgumentException("Treść odpowiedzi nie może być pusta!");
            if ((from a in answers where a.IsCorrect == true select a).Count() != 1)
                throw new System.ArgumentException("Jedna odpowiedź musi być prawidłowa!");
            if ((from a in answers where a.IsSelected == true select a).Count() != 0)
                throw new System.ArgumentException("Nie możesz dodać odpowiedzi która jest już zaznaczona");
            if (answers.Length != Game.numberOfAnswers) // it is like answers.Length == Answers.Length
                throw new System.ArgumentException("Za dużo możliwych odpowiedzi do wybrania!");


            #endregion

            Title = title;
            order = _order;

            // Be aware of sending refference!
            for (int i = 0; i < Game.numberOfAnswers; i++)
                Answers[i] = (Answer)answers[i].Clone(); // I do not want assing reffer, so i have to mace answer clone!
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
                Console.WriteLine($"Treść odpowiedzi numer: [{i + 1}]");
                string tempAnswer = Console.ReadLine();

                Validators.ValidString(tempAnswer, 6, "Wprowadź treść odpowiedzi ponownie");
                Answers[i] = new Answer(tempAnswer);
            }

            Console.Clear();

            for (int i = 0; i < Answers.Length; i++)
            {
                Console.Write($"[{i}]. {Answers[i].Title}\n");
            }

            SetCorrectAnswer();
            Console.WriteLine();
        }

        /// <summary>
        /// Choose one correct answer from created answers
        /// </summary>
        private void SetCorrectAnswer()
        {
            Console.Write("\nOkej... Teraz musisz wybrać która odpowiedź jest prawidłowa: ");

            string input = Console.ReadLine();

            // Validate input
            int intInput;
            while (!Int32.TryParse(input, out intInput)
                || intInput < 0
                || intInput > Answers.Length - 1)
            {
                Console.Clear();
                Console.WriteLine("Wybacz... Błędnie wprowadzone dane, wprowadź numer poprawnej odpowiedzi ponownie");

                for (int i = 0; i < Answers.Length; i++)
                {
                    Console.Write($"[{i}]. {Answers[i].Title}\n");
                }

                input = Console.ReadLine();
            }

            // Set correct answer
            Answers[intInput].IsCorrect = true;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Select correct answer, and get information does this select is correct.
        /// </summary>
        /// <param name="answerIndex"></param>
        /// <returns></returns>
        public bool SelectAndCheckAnswer(int answerIndex)
        {
            Answers[answerIndex].IsSelected = true;
            return Answers[answerIndex].IsSelected == Answers[answerIndex].IsCorrect;
        }

        #endregion
    }

    [Serializable]
    public sealed class Answer : ICloneable
    {
        #region Constructor

        public Answer(string title, bool isCorrect = false)
        {
            IsCorrect = isCorrect;
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
