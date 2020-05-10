using NUnit.Framework;
using QuizApp.Model;

namespace QuizApp.UnitTests.pre_test
{
    public class BaseClass
    {
        protected readonly GameConfiguration gameConfiguration = new GameConfiguration(4, 4, 4, 4, "test.txt");
    }
}
