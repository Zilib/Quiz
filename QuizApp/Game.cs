using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{

    class Game
    {
        #region Public static readonly

        public static bool testsAvailable { get; } = true;
        public static int numberOfAnswers { get; } = 4;
        public static int minQuestions { get; } = 2;
        public static int maxQuestions { get; } = 10;
        public static int minTitleLength { get; } = 4;
        public static int minDescriptionLength { get; } = 15;

        #endregion

        #region Private readonly (Config)

        private readonly string saveFileName;

        #endregion

        #region Props

        public List<Quiz> Quizes { get => quizes; }

        #endregion

        #region Private variables

        // All quizes
        private List<Quiz> quizes = new List<Quiz>();

        private Quiz selectedQuiz = null;

        private int playerScore = 0;

        #endregion

        #region Public lambda methods 

        public int GetNumberOfQuizes => quizes.Count;

        #endregion

        #region Public methods

        public void SetQuizes(List<Quiz> _quizes)
        {
            if (_quizes.Count == 0)
                throw new System.Exception("Quizes are empty!");
        }

        public void CreateNewQuiz()
        {
            Console.Clear();
            Quiz tempQuiz = new Quiz();

            #region Set quiz main data

            Console.WriteLine("Please input quiz title");
            tempQuiz.Title = Console.ReadLine();
            Validators.ValidString(tempQuiz.Title, 4, "Please input quiz title");

            Console.Clear();
            Console.WriteLine("Please input quiz description");
            tempQuiz.Description = Console.ReadLine();
            Validators.ValidString(tempQuiz.Description, 20, "Please input quiz description");

            #endregion

            #region Create Questions

            Console.Clear();
            Console.WriteLine("How many questions do you want? ( More than 2 and less than 10)");

            string input = Console.ReadLine();
            // Untill input is not int or is larger than 10 or smaller than 2
            int numberOfQuestions;
            while (!Int32.TryParse(input, out numberOfQuestions)
                || (numberOfQuestions <= 2
                || numberOfQuestions >= 10))
            {
                Console.Clear();
                Console.WriteLine("Wrong input!");
                Console.WriteLine("How many questions do you want? ( More than 2 and less than 10)");
                input = Console.ReadLine();
            }

            for (int i = 0; i < numberOfQuestions; i++)
            {
                tempQuiz.CreateQuestion();
            }

            #endregion

            quizes.Add(tempQuiz);
        }

        public void SelectQuiz()
        {
            Console.Clear();

            if (quizes.Count == 0)
            {
                Console.WriteLine("No created quiz!");
                return;
            }

            Console.Clear();
            Console.WriteLine("Select your quiz! ");
            ShowQuizes();

            string input = Console.ReadLine();
            int intInput;
            while (!Int32.TryParse(input, out intInput)
                || intInput < 0
                || intInput > quizes.Count)
            {
                Console.Clear();
                Console.WriteLine("Incorrect input! Please choose quiz again!");
                ShowQuizes();
                input = Console.ReadLine();
            }

            // Assign by reffer
            selectedQuiz = quizes[intInput];

            selectedQuiz.AnswerQuestions(ref playerScore);

            selectedQuiz.ShowAllQuestionsAndAnsers();
        }

        private void ShowQuizes()
        {
            for (int i = 0; i < quizes.Count; i++)
            {
                Console.WriteLine($"[{i}]. {quizes[i].Title}");
            }
        }

        public void CreateQuizTest()
        {
            if (!Game.testsAvailable)
                throw new System.ArgumentException("Sorry but this metod is unable to use");

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
                new Answer("Druga odpowiedź"),
                new Answer("Trzecia odpowiedź"),
                new Answer("Czwarta odpowiedź"),
            };

            answersText[2].IsCorrect = true;

            #endregion

            Random correctAnswer = new Random();
        
            Question[] questions = new Question[4];
            for (int i = 0; i < questions.Length; i++)
            {
                questions[i] = new Question(titles.Peek(), i);
                titles.Dequeue();
                // Make random answer correct answer
                try
                {
                    questions[i].SetAnswers(answersText);
                }
               catch (System.ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                
            }
            CreateNewQuiz("Mój przykładowy quiz", "Mój pierwszy quiz!", questions);
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
                        quizes = tempQuzies;
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("Quizes.dat file is damaged!");
                        Console.ReadLine();
                    }
                }
                catch (System.Runtime.Serialization.SerializationException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Quizes file is damaged!");
                    Console.ResetColor();
                }
            }
        }

        #endregion

        #region Private methods

        private void CreateNewQuiz(string title, string description, Question[] questions)
        {
            Quiz tempQuiz = new Quiz();

            #region Validate arguments, and set quiz main data

            // Validate questions array
            if (questions == null)
                throw new System.ArgumentException("Qusetions are null");
            if (questions.Length == 0)
                throw new System.ArgumentException("Questions are empty");

            if (title == string.Empty)
                throw new System.ArgumentException("Title cannot be empty!");
            if (title.Length < Game.minTitleLength)
                throw new System.ArgumentException("Title is too short!");

            if (description == string.Empty)
                throw new System.ArgumentException("Description cannot be empty!");
            if (description.Length < Game.minDescriptionLength)
                throw new System.ArgumentException("Description is too short!");

            #endregion

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

            quizes.Add(tempQuiz);
        }

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

        public Game(List<Quiz> quizesToLoad, string _saveFileName = "Quizes.dat") : this(_saveFileName)
        {
            if (quizesToLoad.Count > 0)
                quizes = quizesToLoad;
        }

        #endregion
    }
}
