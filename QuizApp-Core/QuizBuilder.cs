﻿using QuizApp.Models;
using System;

namespace QuizApp
{
    public sealed class QuizBuilder
    {
        private readonly Game _game;
        private readonly GameConfiguration _gameConfiguration;

        public QuizBuilder(Game game)
        {
            _game = game;
            _gameConfiguration = game.gameConfiguration;
        }

        public QuizBuilder BuildQuiz()
        {
            Console.Clear();
            Console.WriteLine("Input your quiz name: ");
            string quizTitle = Console.ReadLine() ?? "";
            var newQuiz = _game.CreateNewQuiz(quizTitle);

            BuildQuestions(newQuiz);

            return this;
        }

        public QuizBuilder BuildQuestions(Quiz quizToBuild)
        {
            Console.Clear();
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
                Console.Clear();
                Console.WriteLine("Insert title of question!");
                string questionTitle = Console.ReadLine();

                try
                {
                    var newQuestion = quizToBuild.CreateNewQuestion(questionTitle);

                    for (int j = 0; j < _gameConfiguration.numberOfAnswers; j++)
                    {
                        Console.Clear();
                        Console.WriteLine($"Give text of {j + 1} answer");
                        string answerText = Console.ReadLine();
                        newQuestion.CreateNewAnswer(answerText);
                    }
                    // todo move it from controller
                    SelectCorrectAnswer(newQuestion);
                }
                catch (Exception ex)
                {
                    _game.RemoveQuiz(quizToBuild);
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }
            }

            return this;
        }

        private static void SelectCorrectAnswer(Question question)
        {
            Console.Clear();
            Console.WriteLine("Which answer is correct?");
            var answers = question.Answers;
            for (int i = 0; i < answers.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {answers[i].Text}");
            }

            int intInput;
            while (!int.TryParse(Console.ReadLine(), out intInput))
            {
                Console.Clear();
                Console.WriteLine("Incorrect answer's index!");
                for (int i = 0; i < answers.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}]. {answers[i].Text}");
                }
            }
            var answerToSetCorrect = question.Answers[intInput - 1];
            question.SetCorrectAnswer(answerToSetCorrect);
        }
    }
}