using QuizApp.Model;
using System;
using System.Collections.Generic;
using QuizApp.Services;
using QuizApp.View.Services;
using QuizApp.View;

namespace QuizApp.Views
{
    public sealed class GameView
    {
        private readonly Game _game;
        private readonly QuizViewService _quizViewService;
        public GameView(Game game)
        {
            _game = game;
            _quizViewService = new QuizViewService(game);
        }

        public void Play()
        {
            Quiz currentQuiz;
            List<Question> questions;
            try
            {
                currentQuiz = _quizViewService.SelectQuiz("Select quiz!");
                questions = currentQuiz.Questions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }
           
            foreach (var question in questions)
            {
                Console.Clear();
                Console.WriteLine(question.Title + "\n");
                _quizViewService.SelectAnswer(question);
            }

            _quizViewService.ShowUserAnswers(questions);
            Console.ReadLine();
        }

        public void Edit()
        {
            EditView editView = new EditView(_quizViewService);
            editView.Start();
        }

        
        public void RemoveQuiz()
        {
            Quiz quizToRemove;
            try
            {
                quizToRemove = _quizViewService.SelectQuiz("Select quiz to remove!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }
            _game.RemoveQuiz(quizToRemove);
        }

    }
}
