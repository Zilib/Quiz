using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace QuizApp
{

    class Game
    {
        #region Public static readonly

        public static int numberOfAnswers { get; } = 4;
        public static int minQuestions { get; } = 2;
        public static int maxQuestions { get; } = 10;
        public static int minTitleLength { get; } = 4;
        public static int minDescriptionLength { get; } = 15;

        #endregion

        #region Private readonly (Config)

        private readonly string saveFileName;
        private static bool testsAvailable { get; } = true;

        #endregion

        #region Props

        // All quizes
        public List<Quiz> Quizes { get; private set; } = new List<Quiz>();

        #endregion

        #region Private variables

        // Reffer to selected quiz
        private Quiz selectedQuiz = null;

        // Not used right now
        private int playerScore = 0;

        #endregion

        #region Public methods

        public void AnswerForQuestions()
        {
            SelectQuiz();

            if (selectedQuiz == null)
                throw new System.ArgumentException("Nie wybrałeś quizu!");

            selectedQuiz.AnswerQuestions(ref playerScore);

            selectedQuiz.ShowAllQuestionsAndAnsers();
        }

        public void CreateNewQuiz()
        {
            Console.Clear();
            Quiz tempQuiz = new Quiz();

            #region Set quiz main data

            Console.WriteLine("Wprowadź tytuł quizu!");
            tempQuiz.Title = Console.ReadLine();
            Validators.ValidString(tempQuiz.Title, 4, "Wprowadź tytuł quizu!");

            Console.Clear();
            Console.WriteLine("Wprowadź opis quizu");
            tempQuiz.Description = Console.ReadLine();
            Validators.ValidString(tempQuiz.Description, 20, "Wprowadź opis quizu");

            #endregion

            #region Create Questions

            Console.Clear();
            Console.WriteLine($"Ile pytań quiz ma zawierać? ( Więcej niż {minQuestions} i mniej niż {maxQuestions})");

            string input = Console.ReadLine();
            // Untill input is not int or is larger than 10 or smaller than 2
            int numberOfQuestions;
            while (!Int32.TryParse(input, out numberOfQuestions)
                || (numberOfQuestions <= 2
                || numberOfQuestions >= 10))
            {
                Console.Clear();
                Console.WriteLine("Wprowadzono błędne dane!!");
                Console.WriteLine($"Ile pytań quiz ma zawierać? ( Więcej niż {minQuestions} i mniej niż ${maxQuestions})");
                input = Console.ReadLine();
            }

            for (int i = 0; i < numberOfQuestions; i++)
            {
                tempQuiz.CreateQuestion();
            }

            #endregion

            Quizes.Add(tempQuiz);
        }

        private void ShowQuizes()
        {
            for (int i = 0; i < Quizes.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]. {Quizes[i].Title}");
            }
        }

        public void CreateQuizTest()
        {
            if (!testsAvailable)
                throw new System.ArgumentException("Sorry but this metod is unable to use"); // Eng because this error should be only able for developers

            #region Queue titles

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

            #endregion

            #region Answers

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

            #endregion

        
            CreateNewQuiz("Mój przykładowy quiz", "Mój pierwszy quiz!", tempQuestions.ToArray());
        }

        /// <summary>
        /// Save game into Quizes.dat file
        /// </summary>
        public void SaveGame()
        {
            BinaryFormatter binFormat = new BinaryFormatter();

            using (Stream fStream = new FileStream(saveFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, Quizes);
            }
        }

        /// <summary>
        /// Load existing Quizes.dat file
        /// </summary>
        public void LoadGame()
        {
            if (!File.Exists(saveFileName)) // Go back if questions doesn't exist
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

        #endregion

        #region Private methods

        /// <summary>
        /// Select quiz and save it to the selectedQuiz variable by reffer
        /// </summary>
        private void SelectQuiz()
        {
            Console.Clear();

            if (Quizes.Count == 0)
            {
                Console.WriteLine("No created quiz!");
                return;
            }

            Console.Clear();
            Console.WriteLine("Wybierz quiz! ");
            ShowQuizes();

            string input = Console.ReadLine();
            int intInput;
            while (!Int32.TryParse(input, out intInput)
                || --intInput < 0 // Decrement here, right now [1] is first index
                || intInput > Quizes.Count)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input! Please choose quiz again!");
                ShowQuizes();
                input = Console.ReadLine();
            }

            // Assign by reffer
            selectedQuiz = Quizes[intInput];
        }

        /// <summary>
        ///  Only available for test method
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="questions"></param>
        private void CreateNewQuiz(string title, string description, Question[] questions)
        {
            Quiz tempQuiz = new Quiz();

            #region Validate arguments, and set quiz main data

            // Validate questions array
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

            #endregion

            try
            {
                CheckQuiz(tempQuiz);
                // Set quiz data
                tempQuiz.Title = title;
                tempQuiz.Description = description;
                tempQuiz.SetQuestions(questions.ToList());
            }
            catch (System.ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            // Load it into application
            Quizes.Add(tempQuiz);
        }

        /// <summary>
        /// Check does every quizes has filled data
        /// </summary>
        /// <param name="_quizes"></param>
        private void CheckQuiz(List<Quiz> _quizes)
        {
            if (_quizes == null 
                || ((from q in _quizes where q.Title == String.Empty 
                     || q.Description == String.Empty select q).Count() > 0))
                throw new ArgumentException("Title or description of question cannot be empty!");


            if ((from q in _quizes where q.Questions == null select q).Count() != 0)
                throw new ArgumentException("Question cannot be null");

            // If every answers 
            if ((from q in _quizes where q.ExistEmptyAnswer() == true select q).Count() != 0)
                throw new ArgumentException("Answers cannot be empty");
        }

        /// <summary>
        /// Check does every data in quiz is filled
        /// </summary>
        /// <param name="_quiz"></param>
        private void CheckQuiz(Quiz _quiz)
        {
            #region Check main data of Quiz

            if (_quiz == null)
                throw new System.ArgumentException("Quiz cannot be null");

            if (_quiz.Questions == null )
                throw new System.ArgumentException("Questions cannot be null");

            if (_quiz.ExistEmptyAnswer())
                throw new System.ArgumentException("Answers cannot be empty");

            #endregion

        }

        #endregion

        #region Constructs

        public Game(string _saveFileName = "Quizes.dat") 
        {
            saveFileName = _saveFileName;
            LoadGame();
        }

        #endregion
    }
}
