using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp
{
    [Serializable]
    sealed class Quiz
    {

        #region Static readonly

        public static int minQuestions { get; } = 2;
        public static int maxQuestions { get; } = 10;
        public static int minTitleLength { get; } = 4;
        public static int minDescriptionLength { get; } = 15;

        #endregion

        #region Private params

        private List<Question> questions = new List<Question>();

        #endregion

        #region Public params

        public string Title { get; set; }
        public string Description { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fill basic question data, and make 4 available answers for it
        /// </summary>
        public void CreateQuestion()
        {
            Console.Clear();
            Console.WriteLine("Give your question text:");

            string tempText = Console.ReadLine();
            Validators.ValidString(tempText, 10, "Give your text:");

            questions.Add(new Question(tempText, questions.Count));
        }

        /// <summary>
        /// Method available wheneveronly developer allow for it, serve for create questions instantly without user.
        /// </summary>
        /// <param name="_questions"></param>
        public void SetQuestions(List<Question> _questions)
        {
            if (!Game.testsAvailable)
                throw new System.ArgumentException("Sorry but this metod is unable to use");
            if (_questions.Count > maxQuestions)
                throw new System.ArgumentException("A lot of questions!");
            if (_questions.Count < minQuestions)
                throw new System.ArgumentException("You have to add more questions!");
            // Check does answers are filled, and check does there is at least one correct answer
            foreach (Question q in _questions)
            {
                if (!q.isAnswersFilled())
                    throw new System.ArgumentException($"\"{q.Title}\" has not filled answers!");

                if (!q.existOneCorrectAnswer())
                    throw new System.ArgumentException($"\"{q.Title}\" does not have one correct answer!");

                questions.Add(q);
            }
        }

        public void AnswerQuestions(ref int quizScore)
        {
            foreach (Question q in questions)
            {
                Console.Clear();
                Console.WriteLine($"\n\n\t\t**********\t{q.Title.ToUpper()}\t**********\n");
                Console.WriteLine("Make your choose!");


                for (int i = 0; i < q.answers.Count; i++)
                {
                    Console.Write($"[{i}]. {q.answers[i].Title}\n");
                }

                string input = Console.ReadLine();
                int intInput;
                while (!Int32.TryParse(input, out intInput) 
                    || intInput >= Question.numberOfAnswers 
                    || intInput < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Sorry invalid input, choose you answer again!");
                    for (int i = 0; i < q.answers.Count; i++)
                    {
                        Console.Write($"[{i}]. {q.answers[i].Title}\n");
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
            
            foreach (Question q in questions)
            {
                if (q.questionHasAnswer != 1)
                {
                    Console.WriteLine("You have to make answer for every question");
                    return;
                }

            }

            ConsoleColor prevBGColor = Console.BackgroundColor;
            ConsoleColor wrongAnswerBGColor = ConsoleColor.Red;
            ConsoleColor correctAnswerBGColor = ConsoleColor.Green;

            ConsoleColor prevFGColor = Console.ForegroundColor;
            ConsoleColor wrongAnswerFGColor = ConsoleColor.Black;
            ConsoleColor correctAnswerFGColor = ConsoleColor.Black;

            // If any answer has no answer, or any answer has more than one answer

            foreach (Question q in questions)
            {
                Console.ForegroundColor = prevFGColor;
                Console.BackgroundColor = prevBGColor;
                Console.WriteLine($"\n\n\t\t**********\t{q.Title.ToUpper()}\t**********\n");
                for (int i = 0; i < q.answers.Count; i++)
                {
                    // If answer is selected and incorrect
                    if (q.answers[i].IsSelected
                        && q.answers[i].IsSelected != q.answers[i].IsCorrect)
                    {
                        Console.BackgroundColor = wrongAnswerBGColor;
                        Console.ForegroundColor = wrongAnswerFGColor;
                        Console.Write($"\t[{i}]. {q.answers[i].Title}\n");
                        continue;
                    }
                    // Correct answer
                    if (q.answers[i].IsSelected 
                        && q.answers[i].IsSelected == q.answers[i].IsCorrect)
                    {
                        Console.ForegroundColor = correctAnswerFGColor;
                        Console.BackgroundColor = correctAnswerBGColor;
                        Console.Write($"\t[{i}]. {q.answers[i].Title}\n");
                        continue;
                    }
                    Console.ForegroundColor = prevFGColor;
                    Console.BackgroundColor = prevBGColor;
                    Console.Write($"\t[{i}]. {q.answers[i].Title}\n");
                }
                // Not selected answer, and not correct
                Console.BackgroundColor = prevFGColor;
                Console.BackgroundColor = prevBGColor;
                Console.WriteLine();
            }
        Console.ReadLine();
        }

        #endregion
    }

}