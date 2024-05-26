using Engine.Events;

namespace Engine.SystemSettings
{
    public abstract class ChangeAccess : IRunnable
    {
        public bool Enabled { get; set; }

        public abstract void Run();
    }
}
