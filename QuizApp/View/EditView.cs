using QuizApp.Model;
using QuizApp.View.Services;
using System;
using System.Collections.Generic;


namespace QuizApp.View
{
    public class EditView
    {
        private readonly QuizViewService _quizViewService;
        private Quiz quizToEdit;
        public EditView(QuizViewService quizViewService)
        {
            _quizViewService = quizViewService;
        }

        private void EditMenu()
        {
            Console.Clear();
            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("[1]. Edit quiz title");
            Console.WriteLine("[2]. Edit any question content.");
            Console.WriteLine("[3]. Go back");
            if (!int.TryParse(Console.ReadLine(), out int output) || output <= 0 || output > 3)
            {
                throw new Exception("Invalid input");
            }

            if (output == 3)
            {
                return;
            }

            var actions = new Dictionary<int, Action>
            {
                {1, EditQuizTitle },
                {2, EditQuestionMenu }
            };
            actions[output].Invoke();
        }

        private void EditQuestionMenu()
        {
            var selectedQuestion = SelectQuestion();
            Console.Clear();
            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("[1]. Edit question title");
            Console.WriteLine("[2]. Edit answer.");
            Console.WriteLine("[3]. Go back");
            if (!int.TryParse(Console.ReadLine(), out int output) || output <= 0 || output > 3)
            {
                throw new Exception("Incorrect input!");
            }

            if (output == 3)
            {
                return;
            }
            var actions = new Dictionary<int, Action<Question>>
            {
                {1, EditQuestionTitle },
                {2, EditAnswerMenu }
            };
            actions[output].Invoke(selectedQuestion);
        }

        private void EditQuizTitle()
        {
            Console.Clear();
            Console.WriteLine($"Current quiz title: {quizToEdit.Title}");
            Console.WriteLine("Enter new title!");
            string newTitle = Console.ReadLine();
            quizToEdit.Title = newTitle;
        }

        private Question SelectQuestion()
        {
            Console.Clear();
            var questions = quizToEdit.Questions;
            Console.WriteLine("\nSelect question!");
            for (int i = 0; i < questions.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {questions[i].Title}");
            }
            if (!int.TryParse(Console.ReadLine(), out int output) || output <= 0 || output > questions.Count)
            {
                throw new Exception("Incorrect input");
            }
            return questions[output - 1];
        }

        private void EditQuestionTitle(Question questionToEdit)
        {
            Console.Clear();
            Console.WriteLine($"Current question title: {questionToEdit.Title}");
            Console.WriteLine("Input new name for question:");
            questionToEdit.Title = Console.ReadLine();
        }

        private void EditAnswerMenu(Question questionToEdit)
        {

        }

        public void Start()
        {
            try
            {
                quizToEdit = _quizViewService.SelectQuiz("Select quiz to edit!");
                EditMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }

        }
    }
}
