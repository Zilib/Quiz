using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp
{
    [Serializable]
    sealed class Quiz
    {

        #region Private params

        private List<Question> questions = new List<Question>();

        #endregion

        #region Public lambda methods

        public bool ExistEmptyAnswer() => (from q in questions where q.ExistEmptyAnswer() select q).Count() != 0;

        #endregion

        #region Public params

        public string Title { get; set; }
        public string Description { get; set; }
        public List<Question> Questions { get => questions; }

        #endregion

        #region Private methods

        /// <summary>
        /// Check does every question meet the requirements
        /// </summary>
        /// <param name="_questions"></param>
        private void CheckQuestions(List<Question> _questions)
        {
            if (_questions == null)
                throw new System.ArgumentException("Questions cannot be null!");

            if (_questions.Count > Game.maxQuestions)
                throw new System.ArgumentException("A lot of questions!");

            if (_questions.Count < Game.minQuestions)
                throw new System.ArgumentException("You have to add more questions!");

            foreach (Question q in _questions)
                CheckAnswers(q.Answers.ToList());
        }

        /// <summary>
        /// Check does answers are created correctly.
        /// </summary>
        /// <param name="_answers">list with answers</param>
        private void CheckAnswers(List<Answer> _answers)
        {
            if (_answers == null)
                throw new System.ArgumentException("Answers cannot be null!");

            if (_answers.Count != Game.numberOfAnswers)
                throw new System.ArgumentException("Invalid number of answers!");

            if ((from a in _answers where a.IsCorrect == true select a).Count() != 1)
                throw new System.ArgumentException("Answer should has one available answer to choose");
        }

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
        /// Set immediately question list
        /// </summary>
        /// <param name="_questions"></param>
        public void SetQuestions(List<Question> _questions)
        {
            CheckQuestions(_questions);
            questions = new List<Question>(_questions);
        }
        
        public void AnswerQuestions(ref int quizScore)
        {
            foreach (Question q in questions)
            {
                Console.Clear();
                Console.WriteLine($"\n\n\t\t**********\t{q.Title.ToUpper()}\t**********\n");
                Console.WriteLine("Make your choose!");


                for (int i = 0; i < q.Answers.Length; i++)
                {
                    Console.Write($"[{i}]. {q.Answers[i].Title}\n");
                }

                string input = Console.ReadLine();
                int intInput;
                while (!Int32.TryParse(input, out intInput) 
                    || intInput >= Game.numberOfAnswers 
                    || intInput < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Sorry invalid input, choose you answer again!");
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
            
            foreach (Question q in questions)
            {
                if (q.questionHasAnswer() != 1)
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
                for (int i = 0; i < q.Answers.Length; i++)
                {
                    // If answer is selected and incorrect
                    if (q.Answers[i].IsSelected
                        && q.Answers[i].IsSelected != q.Answers[i].IsCorrect)
                    {
                        Console.BackgroundColor = wrongAnswerBGColor;
                        Console.ForegroundColor = wrongAnswerFGColor;
                        Console.Write($"\t[{i}]. {q.Answers[i].Title}\n");
                        continue;
                    }
                    // Correct answer
                    if (q.Answers[i].IsSelected 
                        && q.Answers[i].IsSelected == q.Answers[i].IsCorrect)
                    {
                        Console.ForegroundColor = correctAnswerFGColor;
                        Console.BackgroundColor = correctAnswerBGColor;
                        Console.Write($"\t[{i}]. {q.Answers[i].Title}\n");
                        continue;
                    }
                    Console.ForegroundColor = prevFGColor;
                    Console.BackgroundColor = prevBGColor;
                    Console.Write($"\t[{i}]. {q.Answers[i].Title}\n");
                }
                // Not selected answer, and not correct
                Console.BackgroundColor = prevFGColor;
                Console.BackgroundColor = prevBGColor;
                Console.WriteLine();
            }
            Console.ResetColor();
        Console.ReadLine();
        }

        #endregion
    }

}