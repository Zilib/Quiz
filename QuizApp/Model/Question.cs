using QuizApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public sealed class Question
    {
        private readonly Quiz _quiz;
        private readonly Game _quizGame;

        public string Title { get; set; }

        private List<Answer> answers;

        public Question(Quiz quiz, string title, Game game)
        {
            _quizGame = game;
            Title = title;
            _quiz = quiz;
            answers = new List<Answer>();
        }

        public Quiz GetMyQuiz() => _quiz;

        public List<Answer> GetAnswers()
        {
            if (answers.Count != _quizGame.gameConfiguration.numberOfAnswers)
            {
                throw new ArgumentOutOfRangeException("Amount of answers is incorrect, amount of answers is: " + _quizGame.gameConfiguration.numberOfAnswers);
            }

            return answers;
        }

        public bool ExistCorrectAnswer() => answers.Select(x => x).Where(x => x.IsCorrect == true).Count() == 1;

        public bool ExistSelectedAnswer() => answers.Select(x => x).Where(x => x.IsSelected == true).Count() == 1;

        public Answer CreateNewAnswer(string text)
        {
            if (text == string.Empty || text == null)
            {
                throw new IncorrectInputException("Answer text cannot be null");
            }
            if (answers.Count == _quizGame.gameConfiguration.numberOfAnswers)
            {
                throw new IncorrectInputException("You cannot add anymore answers!");
            }
            Answer answerToAdd = new Answer(this, text);
            answers.Add(answerToAdd);

            return answerToAdd;
        }

        public void ShowAnswers()
        {
            Console.WriteLine("Select correct answer:");
            for (int i = 0; i < answers.Count(); i++)
            {
                Console.WriteLine($"[{i + 1}]. {answers[i]}");
            }
        }

        public void SelectCorrectAnswer()
        {
            Console.Clear();
            ShowAnswers();

            string input = Console.ReadLine();
            int correctAnswerIndex = 0;

            while (!int.TryParse(input, out correctAnswerIndex) 
                 || correctAnswerIndex - 1 > answers.Count() - 1)
            {
                Console.Clear();
                Console.WriteLine("Index of answer is incorrect! Please input index again!");
                ShowAnswers();
            }

            answers[correctAnswerIndex - 1].IsCorrect = true;
        }
    }
}
