using log4net;

namespace Test_Taste_Console_Application.Utilities
{
    //This is a static utility class that provides a centralized logging mechanism for the application using the log4net logging framework.
    public static class Logger
    {
        public static readonly ILog Instance = LogManager.GetLogger(typeof(Program));
    }
}
