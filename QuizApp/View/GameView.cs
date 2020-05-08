using QuizApp.Fascade;
using QuizApp.Model;
using System;
using System.Collections.Generic;

namespace QuizApp.Views
{
    public sealed class GameView
    {
        private readonly GameViewModel _gameFascade;

        public GameView(GameViewModel gameFascade)
        {
            _gameFascade = gameFascade;
        }

        private Quiz SelectQuiz()
        {
            List<Quiz> quizes = _gameFascade.GetQuizes(true); 

            Console.WriteLine("Select quiz!");
            for (int i = 0; i < quizes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {quizes[i].Title}");
            }
            Console.WriteLine();
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                var quizToReturn = quizes[input - 1];
                return quizes[input - 1];
            }
            else
            {
                Console.WriteLine("Incorrect number!");
                return null;
            }
        }

        public void PlayQuiz()
        {
            var currentQuiz = SelectQuiz();
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

        private void ShowUserAnswers(List<Question> questions)
        {
            Console.Clear();
            foreach (var question in questions)
            {
                Console.WriteLine();
                Console.WriteLine(question.Title);
                Console.WriteLine();

                if (!question.ExistSelectedAnswer())
                {
                    Console.WriteLine("No selected answer!");
                    Console.ReadLine();
                    return;
                }
                var answers = question.Answers;
                foreach (var answer in answers)
                {
                    if (answer.GetState() == EAnswerState.Incorrect)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(answer);
                        Console.ResetColor();
                    }
                    else if (answer.GetState() == EAnswerState.Correct)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(answer);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(answer);
                    }
                }
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
