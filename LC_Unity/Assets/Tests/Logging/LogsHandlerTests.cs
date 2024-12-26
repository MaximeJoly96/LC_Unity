using Logging;
using NUnit.Framework;

namespace Testing.Logging
{
    public class LogsHandlerTests : TestFoundation
    {
        [Test]
        public void LogInformationTest()
        {
            LogsHandler.Instance.Log("Information message");
        }

        [Test]
        public void LogWarningTest()
        {
            LogsHandler.Instance.Log("Warning message");
        }

        [Test]
        public void LogErrorTest()
        {
            LogsHandler.Instance.Log("Error message");
        }

        [Test]
        public void LogFatalErrorTest()
        {
            LogsHandler.Instance.Log("Fatal error message");
        }
    }
}
