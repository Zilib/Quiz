﻿using QuizApp.Fascade;
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
        private readonly GameFascade _gameFascade;
        private readonly GameConfiguration _gameConfiguration;

        public QuizTestExample(GameFascade gameFascade)
        {
            _gameFascade = gameFascade;
            _gameConfiguration = _gameFascade.GetGameConfiguration();
        }

        public void CreateTestQuiz()
        {
            _gameFascade.CreateNewQuiz("Mój pierwszy testowy quiz");

            var currentQuestion  = _gameFascade.CreateNewQuestion("Jak ma na imię autor aplikacji?");

            _gameFascade.CreateNewAnswer(currentQuestion, "Adam");
            var correctAnswer = _gameFascade.CreateNewAnswer(currentQuestion, "Adrian");
            _gameFascade.CreateNewAnswer(currentQuestion, "Jakub");
            _gameFascade.CreateNewAnswer(currentQuestion, "Andrzej");

            currentQuestion.SelectCorrectAnswer(correctAnswer);

            currentQuestion = _gameFascade.CreateNewQuestion("Z jakiego miasta pochodzi autor aplikacji?");

            correctAnswer = _gameFascade.CreateNewAnswer(currentQuestion, "Lublin");
            _gameFascade.CreateNewAnswer(currentQuestion, "Warszawa");
            _gameFascade.CreateNewAnswer(currentQuestion, "Kraków");
            _gameFascade.CreateNewAnswer(currentQuestion, "Łódź");

            currentQuestion.SelectCorrectAnswer(correctAnswer);

            currentQuestion = _gameFascade.CreateNewQuestion("Ile lat ma autor aplikacji?");

            _gameFascade.CreateNewAnswer(currentQuestion, "18");
            _gameFascade.CreateNewAnswer(currentQuestion, "19");
            correctAnswer = _gameFascade.CreateNewAnswer(currentQuestion, "20");
            _gameFascade.CreateNewAnswer(currentQuestion, "21");

            currentQuestion.SelectCorrectAnswer(correctAnswer);
        }
    }
#endif
}