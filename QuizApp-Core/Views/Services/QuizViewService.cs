using QuizApp.Models;
using QuizApp.Services;
using System;
using System.Collections.Generic;

namespace QuizApp.View.Services
{
    public class QuizViewService
    {
        private readonly Game _game;
        public QuizViewService(Game game)
        {
            _game = game;
        }

        private ConsoleColor ColorForAnswer(EAnswerState? state)
        {
            switch (state)
            {
                case EAnswerState.Correct:
                    return ConsoleColor.Green;
                case EAnswerState.Incorrect:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.White;
            }
        }

        public Quiz SelectQuiz(string msg)
        {
            Console.Clear();
            List<Quiz> quizes = _game.GetQuizes(true);

            Console.WriteLine(msg);
            for (int i = 0; i < quizes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {quizes[i].Title}");
            }
            Console.WriteLine();
            if (!int.TryParse(Console.ReadLine(), out int output) || output <= 0 || output > quizes.Count)
            {
                throw new Exception("Incorrect input");
            }
            return quizes[output - 1];
        }

        public void ShowUserAnswers(List<Question> questions)
        {
            Console.Clear();
            foreach (var question in questions)
            {
                Console.WriteLine($"\n{question.Title}\n");

                if (!question.IsAnyAnswerSelected())
                {
                    Console.WriteLine("No selected answer!");
                    Console.ReadLine();
                    return;
                }
                var answers = question.Answers;
                foreach (var answer in answers)
                {
                    ConsoleColor? foregroundColor = ColorForAnswer(answer.GetState());
                    ConsoleWriter.WriteInForegroundColor(answer.Text, foregroundColor);
                }
            }
        }

        public void SelectAnswer(Question question)
        {
            int answerIndex;
            var answers = question.Answers;

            Console.WriteLine("Select correct answer!");
            for (int i = 0; i < answers.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {answers[i]}");
            }
            string input = Console.ReadLine();

            while (!int.TryParse(input, out answerIndex))
            {
                Console.WriteLine("Select correct answer!");
                for (int i = 0; i < answers.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}]. {answers[i]}");
                }
            }
            var answerToSelect = answers[answerIndex - 1];
            question.SelectAnswer(answerToSelect);
        }
    }
}