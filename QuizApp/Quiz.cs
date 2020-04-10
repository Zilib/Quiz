using System;
using System.Collections;
using System.Collections.Generic;

namespace QuizApp
{

    sealed class Quiz
    {
        #region Static readonly
        public static int minQuestions { get; } = 2;
        public static int maxQuestions { get; } = 10;
        public static int minTitleLength { get; } = 4;
        public static int minDescriptionLength { get; } = 15;

        #endregion

        #region Private params

        private List<Question> questions = new List<Question>();

        #endregion

        #region Public params

        public string Title { get; set; }
        public string Description { get; set; }

        #endregion

        /// <summary>
        /// Fill basic question data, and make 4 available answers for it
        /// </summary>
        public void CreateQuestion()
        {
            Console.Clear();
            Console.WriteLine("Give your question text:");

            string tempText = Console.ReadLine();
            Validators.ValidString(tempText, 10, "Give your text:");

            questions.Add(new Question(tempText, questions.Count));
        }

        public void SetQuestions(List<Question> _questions)
        {
            if (!Game.testsAvailable)
                throw new System.ArgumentException("Sorry but this metod is unable to use");
            if (_questions.Count > maxQuestions)
                throw new System.ArgumentException("A lot of questions!");
            if (_questions.Count < minQuestions)
                throw new System.ArgumentException("You have to add more questions!");
            // Check does answers are filled, and check does there is at least one correct answer
            foreach (Question q in _questions)
            {
                if (!q.isAnswersFilled())
                    throw new System.ArgumentException($"\"{q.Text}\" has not filled answers!");

                if (!q.existOneCorrectAnswer())
                    throw new System.ArgumentException($"\"{q.Text}\" does not have one correct answer!");
            }
            questions = _questions;
        }
    }

}