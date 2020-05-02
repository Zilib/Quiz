using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Model
{
    public sealed class Question
    {
        private readonly Quiz _quiz;

        public string Title { get; set; }

        private List<Answer> answers;

        public Question(Quiz quiz, string title)
        {
            Title = title;
            _quiz = quiz;
            answers = new List<Answer>();
        }

        public Quiz GetMyQuiz() => _quiz;

        public List<Answer> GetAnswers() => answers;

        public bool ExistCorrectAnswer() => answers.Select(x => x).Where(x => x.IsCorrect == true).Count() == 1;

        public Answer CreateNewAnswer(string text, bool isCorrect)
        {
            if (text == string.Empty || text == null)
            {
                throw new System.Exception("Answer text cannot be null");
            }
            Answer answerToAdd = new Answer(this, text, isCorrect);
            answers.Add(answerToAdd);

            return answerToAdd;
        }
    }
}
