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
            if (answers.Count != _quizGame.numberOfAnswers)
            {
                throw new System.Exception("Amount of answers is incorrect, amount of answers is: " + _quizGame.numberOfAnswers);
            }

            return answers;
        }

        public bool ExistCorrectAnswer() => answers.Select(x => x).Where(x => x.IsCorrect == true).Count() == 1;

        public bool ExistSelectedAnswer() => answers.Select(x => x).Where(x => x.IsSelected == true).Count() == 1;

        public Answer CreateNewAnswer(string text, bool isCorrect)
        {
            if (text == string.Empty || text == null)
            {
                throw new System.Exception("Answer text cannot be null");
            }
            if (answers.Count == _quizGame.numberOfAnswers)
            {
                System.Console.WriteLine("You cannot add anymore answers!");
                return null;
            }
            Answer answerToAdd = new Answer(this, text, isCorrect);
            answers.Add(answerToAdd);

            return answerToAdd;
        }
    }
}
