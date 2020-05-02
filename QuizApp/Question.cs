using System;
using System.Linq;

namespace QuizApp
{
    [Serializable]
    public sealed class Question
    {
        public string Title { get; set; }
        public Answer[] Answers { get; private set; } = new Answer[Game.numberOfAnswers];

        public bool ExistEmptyAnswer() => (from a in Answers where a.Title == String.Empty select a).Count() != 0;
        public bool ExistOneCorrectAnswer() => (from a in Answers where a.IsCorrect == true select a).Count() == 1;
        public int questionHasAnswer() => (from a in Answers where a.IsSelected == true select a).Count();

        public Question() { }

        public Question(string title)
        {
            Title = title;
            CreateAnswers();
        }

        public Question(string title, int _order, Answer[] answers)
        {

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

            Title = title;

            // Be aware of sending refference!
            for (int i = 0; i < Game.numberOfAnswers; i++)
                Answers[i] = (Answer)answers[i].Clone(); // I do not want assing reffer, so i have to mace answer clone!
        }

        private void CreateAnswers()
        {
            for (int i = 0; i < Game.numberOfAnswers; i++)
            {
                Console.Clear();
                Console.WriteLine($"Treść odpowiedzi numer: [{i + 1}]");
                string tempAnswer = Console.ReadLine();

                Validators.ValidString(tempAnswer, 1, "Wprowadź treść odpowiedzi ponownie");
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

        public bool SelectAndCheckAnswer(int answerIndex)
        {
            Answers[answerIndex].IsSelected = true;
            return Answers[answerIndex].IsSelected == Answers[answerIndex].IsCorrect;
        }

    }

    [Serializable]
    public sealed class Answer : ICloneable
    {
        public Answer(string title, bool isCorrect = false)
        {
            IsCorrect = isCorrect;
            Title = title;
        }

        [NonSerialized]
        private bool isSelected = false;

        public bool IsCorrect { get; set; } = false;
        public bool IsSelected { get => isSelected; set => isSelected = value; }
        public string Title { get; set; }

        public object Clone() => this.MemberwiseClone();

        public bool SelectedIsCorrect()
        {
            if (IsSelected == true && IsCorrect == true)
                return true;
            else if (IsSelected == true && IsCorrect == false)
                return false;
            else if (IsSelected == false && IsCorrect == false)
                return false;

            return false;
        }

    }

}
