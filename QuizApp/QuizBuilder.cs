using QuizApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public sealed class QuizBuilder
    {
        private readonly GameFascade _gameFascade;
        private readonly GameConfiguration _gameConfiguration;

        private Quiz QuizToBuild { get; set; } = null;

        public QuizBuilder(GameFascade gameFascade)
        {
            _gameFascade = gameFascade;
            _gameConfiguration = gameFascade.GetGameConfiguration();
        }

        public QuizBuilder BuildQuiz()
        {
            Console.WriteLine("Input your quiz name: ");
            string quizTitle = Console.ReadLine() ?? "";

            try
            {
                QuizToBuild = _gameFascade.CreateNewQuiz(quizTitle);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return this;
        }

        public QuizBuilder BuildQuestions()
        {
            if (QuizToBuild == null)
            { 
                return this;
            }

            Console.WriteLine("How many questions would you like to have?");
            Console.WriteLine($"Minimum amount of questions: {_gameConfiguration.minQuestions}");
            Console.WriteLine($"Maximum amount of questions: {_gameConfiguration.maxQuestions}");

            string input = Console.ReadLine();
            int amountOfQuestion = 0;

            while (!int.TryParse(input, out amountOfQuestion)
                    || amountOfQuestion > _gameConfiguration.maxQuestions 
                    || amountOfQuestion < _gameConfiguration.minQuestions)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input, please input it again!");
                Console.WriteLine("How many questions would you like to have?");
                Console.WriteLine($"Minimum amount of questions: {_gameConfiguration.minQuestions}");
                Console.WriteLine($"Maximum amount of questions: {_gameConfiguration.maxQuestions}");

                input = Console.ReadLine();
            }

            for (int i = 0; i < amountOfQuestion; i++)
            {
                Console.WriteLine("Insert title of question!");
                string questionTitle = Console.ReadLine();

                try
                {
                    var newQuestion  = _gameFascade.CreateNewQuestion(QuizToBuild, questionTitle);

                    for (int j = 0; j < _gameConfiguration.numberOfAnswers; j++)
                    {
                        Console.Clear();
                        Console.WriteLine($"Give text of {j + 1} answer");
                        string answerText = Console.ReadLine();
                        _gameFascade.CreateNewAnswer(newQuestion, answerText);
                    }

                    newQuestion.SelectCorrectAnswer();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            return this;
        }

        public QuizBuilder Build()
        {
            _gameFascade.AddNewQuiz(QuizToBuild);
            return this;
        }
    }
}
