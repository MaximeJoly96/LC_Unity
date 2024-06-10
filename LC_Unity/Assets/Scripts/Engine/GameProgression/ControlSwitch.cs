using GameProgression;
using UnityEngine;

namespace Engine.GameProgression
{
    public class ControlSwitch : PersistentData
    {
        public bool Value { get; set; }

        public override void Run()
        {
            PersistentDataHolder.Instance.StoreData(Key, Value);
            Debug.Log("stored " + Key);

            Finished.Invoke();
            IsFinished = true;
        }
    }
}
