using QuizApp.Model;

namespace QuizApp.Services.Question
{
    public abstract class QuestionService
    {
        protected abstract GameConfiguration _gameConfiguration { get; }

        public bool IsGameConfigurationRight(GameConfiguration gameConfiguration)
        {
            return _gameConfiguration.Equals(gameConfiguration);
        }
    }
}
