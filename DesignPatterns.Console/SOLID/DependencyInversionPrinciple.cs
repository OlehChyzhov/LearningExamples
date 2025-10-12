namespace DesignPatterns.SOLID
{
    /// <summary>
    /// Dependency Inversion Principle (DIP) states:
    /// High-level modules should not depend on low-level modules.
    /// Both should depend on abstractions.
    /// </summary>

    #region Problem

    public class ConsoleLogger
    {
        /// <summary>
        /// Logs the message to the console.
        /// </summary>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// DataAccessLayer is a high-level module that directly depends on a low-level module (ConsoleLogger).
    /// This makes it hard to switch to other logging mechanisms like file logging.
    /// </summary>
    public class DataAccessLayer
    {
        public void AddCustomer(string name)
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.Log("Customer added: " + name);
        }
    }

    #endregion

    #region Solution

    /// <summary>
    /// Abstraction for logging. Any logging mechanism should implement this interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        void Log(string message);
    }

    public class BetterConsoleLogger : ILogger
    {
        /// <summary>
        /// Logs the message to the console.
        /// </summary>
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class BetterFileLogger : ILogger
    {
        /// <summary>
        /// Logs the message to a file (simulated with console output here).
        /// </summary>
        public void Log(string message)
        {
            // Placeholder: Write message to file.
            Console.WriteLine($"[FileLog] {message}");
        }
    }

    /// <summary>
    /// BetterDataAccessLayer depends on the ILogger abstraction, not on concrete implementations.
    /// This allows us to inject different loggers at runtime.
    /// </summary>
    public class BetterDataAccessLayer
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Pay attention that we used Dependency Injection.
        /// Dependency Injection is a design pattern used to implement the Dependency Inversion Principle.
        /// </summary>
        public BetterDataAccessLayer(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds a customer and logs the action.
        /// </summary>
        public void AddCustomer(string name)
        {
            _logger.Log("Customer added: " + name);
        }
    }
    #endregion

    #region DIP_Example

    /// <summary>
    /// Demonstrates Dependency Inversion Principle by running a sample scenario.
    /// </summary>
    public static class DIP_Example
    {
        public static void Run()
        {
            // Switch between loggers easily
            ILogger logger = new BetterConsoleLogger();
            // ILogger logger = new BetterFileLogger();

            var dataAccess = new BetterDataAccessLayer(logger);
            dataAccess.AddCustomer("John Doe");
        }
    }
    #endregion
}