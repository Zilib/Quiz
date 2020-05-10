using QuizApp.Model;
using System;
using System.Collections.Generic;
using QuizApp.Services;

namespace QuizApp.Views
{
    public sealed class GameView
    {
        private readonly Game _game;

        public GameView(Game game)
        {
            _game = game;
        }

        private Quiz SelectQuiz(string msg)
        {
            List<Quiz> quizes = _game.GetQuizes(true); 

            Console.WriteLine(msg);
            for (int i = 0; i < quizes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {quizes[i].Title}");
            }
            Console.WriteLine();
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                var quizToReturn = quizes[input - 1];
                return quizToReturn;
            }
            else
            {
                Console.WriteLine("Incorrect number!");
                return null;
            }
        }

        public void PlayQuiz()
        {
            var currentQuiz = SelectQuiz("Select quiz!");
            var questions = currentQuiz.Questions;
           
            foreach (var question in questions)
            {
                Console.Clear();
                Console.WriteLine(question.Title + "\n");
                SelectAnswer(question);
            }

            ShowUserAnswers(questions);
            Console.ReadLine();
        }

        public void RemoveQuiz()
        {
            var quizToRemove = SelectQuiz("Select quiz to remove!");
            _game.RemoveQuiz(quizToRemove);
        }

        private void ShowUserAnswers(List<Question> questions)
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

        private void SelectAnswer(Question question)
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
