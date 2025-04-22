namespace BookStore.Services
{
    public interface ILoggerService
    {
        public void Write(string message);
        void LogError(string message);
        void LogInfo(string message);
    }
}
