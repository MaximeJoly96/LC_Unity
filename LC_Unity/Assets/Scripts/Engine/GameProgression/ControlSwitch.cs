using GameProgression;

namespace Engine.GameProgression
{
    public class ControlSwitch : PersistentData
    {
        public bool Value { get; set; }

        public override void Run()
        {
            PersistentDataHolder.Instance.StoreData(Key, Value);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
