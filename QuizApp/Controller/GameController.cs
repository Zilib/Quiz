using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuizApp
{
    partial class Game
    {
        public void AnswerForQuestions()
        {
            if(!SelectQuiz())
            {
                Console.WriteLine("Nie wybrano quizu");
                return;
            }

            selectedQuiz.AnswerQuestions(ref playerScore);

            selectedQuiz.ShowAllQuestionsAndAnsers();
        }

        public void CreateQuizTest()
        {
            if (!testsAvailable)
                throw new System.ArgumentException("Sorry but this metod is unable to use"); 

            Queue<string> titles = new Queue<string>();
            titles.Enqueue("Pierwsze pytanie");
            titles.Enqueue("Drugie pytanie");
            titles.Enqueue("Trzecie pytanie");
            titles.Enqueue("Czwarte pytanie");
            titles.Enqueue("Piąte pytanie");
            titles.Enqueue("Szóste pytanie");
            titles.Enqueue("Siódme pytanie");
            titles.Enqueue("Dziewiąte pytanie");
            titles.Enqueue("Dziesiąte pytanie");
            titles.Enqueue("Jedynaste pytanie");
            titles.Enqueue("Dwunaste pytanie");


            Answer[] answersText = new Answer[4]
            {
                new Answer("Pierwsza odpowiedź"),
                new Answer("Druga odpowiedź", true),
                new Answer("Trzecia odpowiedź"),
                new Answer("Czwarta odpowiedź"),
            };
            List<Question> tempQuestions = new List<Question>();
            try
            {
                tempQuestions.Add(new Question("Pierwsze pytanie", 0, answersText));
                answersText[1].IsCorrect = false;
                answersText[2].IsCorrect = true;
                tempQuestions.Add(new Question("Pierwsze pytanie", 0, answersText));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }



            CreateNewQuiz("Mój przykładowy quiz", "Mój pierwszy quiz!", tempQuestions.ToArray());
        }

        public void SaveGame()
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(saveFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, Quizes);
            }
        }

        public void LoadGame()
        {
            if (!File.Exists(saveFileName))
                return;

            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = File.OpenRead(saveFileName))
            {
                try
                {
                    List<Quiz> tempQuzies = (List<Quiz>)binFormat.Deserialize(fStream);
                    try
                    {
                        CheckQuiz(tempQuzies);
                        Quizes = tempQuzies;
                    }
                    catch (ArgumentException)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Plik \"Quizes.dat\" jest uszkodzony!");
                        Console.ReadLine();
                    }
                }
                catch (System.Runtime.Serialization.SerializationException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Plik \"Quizes.dat\" jest uszkodzony!");
                    Console.ResetColor();
                }
            }
        }

        private bool SelectQuiz()
        {
            Console.Clear();

            if (Quizes.Count == 0)
                return false;

            Console.Clear();
            Console.WriteLine("Wybierz quiz! ");
            ShowQuizes();

            string input = Console.ReadLine();
            int intInput;
            while (!Int32.TryParse(input, out intInput)
                || --intInput < 0
                || intInput > Quizes.Count)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input! Please choose quiz again!");
                ShowQuizes();
                input = Console.ReadLine();
            }

            selectedQuiz = Quizes[intInput];
            return true;
        }

        private void CreateNewQuiz(string title, string description, Question[] questions)
        {
            Quiz tempQuiz = new Quiz();


            if (questions == null)
                throw new System.ArgumentException("Tablica z pytaniami ma wartość null!");
            if (questions.Length == 0)
                throw new System.ArgumentException("Tablica z pytaniami jest pusta!");

            if (title == string.Empty)
                throw new System.ArgumentException("Quiz musi mieć tytuł!");
            if (title.Length < Game.minTitleLength)
                throw new System.ArgumentException("Tytuł quziu jest za krótki!");

            if (description == string.Empty)
                throw new System.ArgumentException("Quiz musi mieć opis!");
            if (description.Length < Game.minDescriptionLength)
                throw new System.ArgumentException("Opis quizu jest za krótki!");


            try
            {
                CheckQuiz(tempQuiz);
                tempQuiz.Title = title;
                tempQuiz.Description = description;
                tempQuiz.SetQuestions(questions.ToList());
            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            Quizes.Add(tempQuiz);
        }

        private void CheckQuiz(List<Quiz> _quizes)
        {
            if (_quizes == null
                || ((from q in _quizes
                     where q.Title == String.Empty
   || q.Description == String.Empty
                     select q).Count() > 0))
                throw new ArgumentException("Title or description of question cannot be empty!");


            if ((from q in _quizes where q.Questions == null select q).Count() != 0)
                throw new ArgumentException("Question cannot be null");

            if ((from q in _quizes where q.ExistEmptyAnswer() == true select q).Count() != 0)
                throw new ArgumentException("Answers cannot be empty");
        }

        private void CheckQuiz(Quiz _quiz)
        {

            if (_quiz == null)
                throw new System.ArgumentException("Quiz cannot be null");

            if (_quiz.Questions == null)
                throw new System.ArgumentException("Questions cannot be null");

            if (_quiz.ExistEmptyAnswer())
                throw new System.ArgumentException("Answers cannot be empty");

        }
    }
}
