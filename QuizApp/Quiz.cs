using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp
{
    /*
    [Serializable]
    sealed class Quiz
    {
        public bool ExistEmptyAnswer() => (from q in Questions where q.ExistEmptyAnswer() select q).Count() != 0;

        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get; private set; } = new List<Question>();

        private void CheckQuestions(List<Question> _questions)
        {
            if (_questions == null)
                throw new System.ArgumentException("Lista pytań nie może mieć wartości null!");

            if (_questions.Count > Game.maxQuestions)
                throw new System.ArgumentException("Za dużo pytań!");

            if (_questions.Count < Game.minQuestions)
                throw new System.ArgumentException("Musisz dodać więcej pytań!");

            foreach (Question q in _questions)
                CheckAnswers(q.Answers.ToList());
        }

        private void CheckAnswers(List<Answer> _answers)
        {
            if (_answers == null)
                throw new System.ArgumentException("Lista odpowiedzi nie może miec wartości null!");

            if (_answers.Count != Game.numberOfAnswers)
                throw new System.ArgumentException("Nieprawidłowa wielkość tablicy z odpowiedziami!");

            if ((from a in _answers where a.IsCorrect == true select a).Count() != 1)
                throw new System.ArgumentException("Lista odpowiedzi powinna zawierać jedną prawidłową odpowiedź!");
        }

        public void CreateQuestion()
        {
            Console.Clear();
            Console.WriteLine("Treść Twojego pytania:");

            string tempText = Console.ReadLine();
            Validators.ValidString(tempText, 10, "Treść Twojego pytania:");

            Questions.Add(new Question(tempText, Questions.Count));
        }

        public void SetQuestions(List<Question> _questions)
        {
            CheckQuestions(_questions);
            Questions = new List<Question>(_questions);
        }
        
        public void AnswerQuestions(ref int quizScore)
        {
            foreach (Question q in Questions)
            {
                Console.Clear();
                Console.WriteLine($"\t\t**********\t{q.Title.ToUpper()}\t**********\n");
                Console.WriteLine("Wybierz odpowiedź!");


                for (int i = 0; i < q.Answers.Length; i++)
                {
                    Console.Write($"[{i + 1}]. {q.Answers[i].Title}\n");
                }

                string input = Console.ReadLine();
                int intInput;
                while (!Int32.TryParse(input, out intInput) 
                    || --intInput >= Game.numberOfAnswers 
                    || intInput < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Błednie wprowadzone dane, wybierz odpowiedź ponownie!");
                    for (int i = 0; i < q.Answers.Length; i++)
                    {
                        Console.Write($"[{i}]. {q.Answers[i].Title}\n");
                    }
                    input = Console.ReadLine();
                }

                if (q.SelectAndCheckAnswer(intInput))
                    quizScore++;
            }
        }

        public void ShowAllQuestionsAndAnsers()
        {
            Console.Clear();
            
            foreach (Question q in Questions)
            {
                if (q.questionHasAnswer() != 1)
                {
                    Console.WriteLine("Musisz odpowiedzieć na wszystkie pytania!");
                    return;
                }

            }

            ConsoleColor prevBGColor = Console.BackgroundColor;
            ConsoleColor wrongAnswerBGColor = ConsoleColor.Red;
            ConsoleColor correctAnswerBGColor = ConsoleColor.Green;

            ConsoleColor prevFGColor = Console.ForegroundColor;
            ConsoleColor wrongAnswerFGColor = ConsoleColor.Black;
            ConsoleColor correctAnswerFGColor = ConsoleColor.Black;


            foreach (Question q in Questions)
            {
                Console.ForegroundColor = prevFGColor;
                Console.BackgroundColor = prevBGColor;
                Console.WriteLine($"\t\t**********\t{q.Title.ToUpper()}\t**********\n");
                for (int i = 0; i < q.Answers.Length; i++)
                {
                    if (q.Answers[i].IsSelected 
                        && !q.Answers[i].SelectedIsCorrect())
                    {
                        Console.BackgroundColor = wrongAnswerBGColor;
                        Console.ForegroundColor = wrongAnswerFGColor;
                        Console.Write($"[{i + 1}]. {q.Answers[i].Title}\n");
                        continue;
                    }
                    if (q.Answers[i].IsSelected 
                        && q.Answers[i].SelectedIsCorrect())
                    {
                        Console.ForegroundColor = correctAnswerFGColor;
                        Console.BackgroundColor = correctAnswerBGColor;
                        Console.Write($"[{i}]. {q.Answers[i].Title}\n");
                        continue;
                    }
                    Console.ForegroundColor = prevFGColor;
                    Console.BackgroundColor = prevBGColor;
                    Console.Write($"[{i}]. {q.Answers[i].Title}\n");
                }
                Console.BackgroundColor = prevFGColor;
                Console.BackgroundColor = prevBGColor;
                Console.WriteLine();
            }
            Console.ResetColor();
        Console.ReadLine();
        }

    }*/

}