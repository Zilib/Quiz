﻿using System;
using System.Collections.Generic;
using System.Linq;
using QuizApp.Model;

namespace QuizApp.Services.Question
{
    public abstract class AnswerService : QuestionService
    {
        public abstract List<Answer> Answers { get; protected set; }

        public Answer CreateNewAnswer(string text)
        {
            if (Answers.Count == _gameConfiguration.numberOfAnswers)
            {
                throw new Exception("You cannot add anymore answers!");
            }
            Answer newAnswer = new Answer(text);
            Answers.Add(newAnswer);

            return newAnswer;
        }

        public void InsertNewAnswer(Answer answerToInsert)
        {
            if (Answers.Count == _gameConfiguration.numberOfAnswers)
            {
                throw new Exception("You cannot add anymore answers!");
            }
            Answers.Add(answerToInsert);
        }

        public void SetCorrectAnswer(Answer correctAnswer)
        {
            if (correctAnswer.IsCorrect)
            {
                throw new Exception("Answer is correct already");
            }
            if (!Answers.Contains(correctAnswer) || IsOneAnswerCorrect())
            {
                throw new ArgumentOutOfRangeException();
            }
            correctAnswer.IsCorrect = true;
        }

        public void SetAllAnswersDefault()
        {
            Answers.Where(x => x.IsSelected).ToList().ForEach(x => x.IsSelected = false);
        }

        private bool IsOneAnswerCorrect()
        {
            return Answers.Select(x => x).Where(x => x.IsCorrect).Count() == 1;
        }

        public bool IsAnyAnswerSelected()
        {
            return Answers.Select(x => x).Where(x => x.IsSelected).Count() == 1;
        }

        public void SelectAnswer(Answer answer)
        {
            if (!Answers.Contains(answer))
            {
                throw new ArgumentOutOfRangeException();
            }
            if (Answers.Count != _gameConfiguration.numberOfAnswers)
            {
                throw new Exception("The number of answers is not equal to number of answers in configuration class.");
            }
            if (IsAnyAnswerSelected())
            {
                throw new Exception("You cannot select twice");
            }
            // todo if correct answer doesn't exist do not allow to select any answer
            answer.IsSelected = true;
        }
    }
}
