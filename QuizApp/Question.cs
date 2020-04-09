﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    sealed class Question : FieldVariables
    {
        private List<Answer> answers = new List<Answer>();
        private int order;

        public Question(string title, int _order)
            : base(title)
        {
            order = _order; // Keep it in mind, it should be ever in this same order.
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
                Console.WriteLine($"Text of answer number {i + 1}");
                string tempAnswer = Console.ReadLine();

                Validators.ValidString(tempAnswer, 6, "Input text of answer again");
                answers.Add(new Answer(tempAnswer));
            }
            Console.Clear();
            ShowAnswers();
            SetCorrectAnswer();
            Console.WriteLine();
        }

        /// <summary>
        /// Choose one correct answer of created answers
        /// </summary>
        private void SetCorrectAnswer()
        {
            Console.Write("\nOkey... You have to select now, which answer is correct: ");

            string input = Console.ReadLine();

            // If input is wrong
            int intInput;
            while (!Int32.TryParse(input, out intInput)
                || intInput < 0
                || intInput > answers.Count - 1)
            {
                Console.Clear();
                Console.WriteLine("Sorry... Incorrect input, select correct answer again");
                ShowAnswers();
                input = Console.ReadLine();
            }

            answers[intInput].isCorrect = true;
        }

        public void ShowAnswers()
        {
            for (int i = 0; i < answers.Count; i++)
            {
                Console.Write($"[{i}]. {answers[i].Text}\n");
            }
        }
    }

    sealed class Answer : FieldVariables
    {
        public Answer(string title)
            : base(title) { }

        public bool isCorrect { get; set; } = false;
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

}
