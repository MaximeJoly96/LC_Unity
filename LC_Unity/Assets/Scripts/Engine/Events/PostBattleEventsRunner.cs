using UnityEngine;

namespace Engine.Events
{
    public class PostBattleEventsRunner : MonoBehaviour
    {
        private void Awake()
        {
            if(EventsRunner.PostBattleEvents != null)
            {
                EventsRunner runner = GetComponent<EventsRunner>();
                EventsSequence sequence = new EventsSequence();
                
                for(int i = 0; i < EventsRunner.PostBattleEvents.Count; i++)
                {
                    sequence.Add(EventsRunner.PostBattleEvents[i]);
                }

                EventsRunner.PostBattleEvents.Clear();

                runner.RunEvents(sequence);
            }
        }
    }
}
