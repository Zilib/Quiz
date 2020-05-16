using QuizApp.Models;

namespace QuizAppTests.UnitTests
{
    public class BaseClass
    {
        protected readonly GameConfiguration gameConfiguration = new GameConfiguration(4, 4, 4, 4, "test.txt");
    }
}
