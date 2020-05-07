using QuizApp.Fascade;
using QuizApp.Model;
using System;
using System.Collections.Generic;

namespace QuizApp.Views
{
    public sealed class GameView
    {
        private readonly GameFascade _gameFascade;

        public GameView(GameFascade gameFascade)
        {
            _gameFascade = gameFascade;
        }

        public void PlayQuiz()
        {
            SelectQuiz();
            if (!_gameFascade.IsQuizSelected())
            {
                Console.WriteLine("Quiz must be selected first!");
                return;
            }
            var currentQuiz = _gameFascade.GetCurrentQuiz();
            var questions = currentQuiz.GetQuestions();
           
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
                var answers = question.GetAnswers();
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
            var answers = question.GetAnswers();

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

            question.SelectAnswer(answerIndex - 1);
        }

        private void SelectQuiz()
        {
            List<Quiz> quizes; 

            try
            {
                quizes = _gameFascade.GetQuizes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Select quiz!");
            for (int i = 0; i < quizes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {quizes[i].Title}");
            }
            Console.WriteLine();
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                _gameFascade.SelectQuiz(input - 1);
            }
            else
            {
                Console.WriteLine("Incorrect number!");
            }
        }
    }
}
