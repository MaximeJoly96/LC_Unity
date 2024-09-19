namespace GameProgression
{
    public class Timer
    {
        public enum State { Uninitialized, Initialized, Running, Paused, Stopped }

        public string Key { get; private set; }
        public float CurrentTime { get; set; }
        public float MaxDuration { get; private set; }
        public State CurrentState { get; private set; }

        public Timer(string key, float maxDuration)
        {
            Key = key;
            MaxDuration = maxDuration;
            CurrentState = State.Initialized;
        }

        public void Run()
        {
            ResetTimer();
            CurrentState = State.Running;
        }

        public void Pause()
        {
            CurrentState = State.Paused;
        }

        public void Stop()
        {
            CurrentState = State.Stopped;
        }

        public void ResetTimer()
        {
            CurrentTime = 0.0f;
        }

        public override string ToString()
        {
            return CurrentTime + "/" + MaxDuration;
        }
    }
}
