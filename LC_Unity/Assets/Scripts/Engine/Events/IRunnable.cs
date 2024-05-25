namespace Engine.Events
{
    /// <summary>
    /// Implement this interface to become runnable in an event routine.
    /// </summary>
    public interface IRunnable
    {
        void Run();
    }
}
