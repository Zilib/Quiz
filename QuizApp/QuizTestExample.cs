using QuizApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
#if DEBUG
    public class QuizTestExample
    {
        private readonly Game _game;

        public QuizTestExample(Game game)
        {
            _game = game;
        }

        public void CreateTestQuiz()
        {
            var newQuiz = _game.CreateNewQuiz("Mój pierwszy testowy quiz");

            var firstQuestion = newQuiz.CreateNewQuestion("Jak ma na imię autor aplikacji?");

            firstQuestion.CreateNewAnswer("Adam");
            firstQuestion.SetCorrectAnswer(firstQuestion.CreateNewAnswer("Adrian"));
            firstQuestion.CreateNewAnswer("Jakub");
            firstQuestion.CreateNewAnswer("Andrzej");

            var secondQuestion = newQuiz.CreateNewQuestion("Z jakiego miasta pochodzi autor aplikacji?");

            secondQuestion.SetCorrectAnswer(secondQuestion.CreateNewAnswer("Lublin"));
            secondQuestion.CreateNewAnswer("Warszawa");
            secondQuestion.CreateNewAnswer("Kraków");
            secondQuestion.CreateNewAnswer("Łódź");

            var thirdQuestion = newQuiz.CreateNewQuestion("Ile lat ma autor aplikacji?");

            thirdQuestion.CreateNewAnswer("18");
            thirdQuestion.CreateNewAnswer("19");
            thirdQuestion.SetCorrectAnswer(thirdQuestion.CreateNewAnswer("20"));
            thirdQuestion.CreateNewAnswer("21");
        }
    }
#endif
}
