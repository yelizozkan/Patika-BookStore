namespace BookStore.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[LOG] {DateTime.Now:HH:mm:ss} - {message}");
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
            Console.ResetColor();
        }

        public void LogInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
            Console.ResetColor();
        }
    }
}
