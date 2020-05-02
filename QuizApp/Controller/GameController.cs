using QuizApp.Model;

namespace QuizApp.Controllers
{
    public class GameController
    {
        private readonly Game _quizGame;
        private Quiz sellectedQuiz { get; set; }

        public GameController(Game quizGame)
        {
            _quizGame = quizGame;
        }
    }

}
