using UnityEngine;

namespace Engine.Events
{
    public class EventsRunner : MonoBehaviour
    {
        [SerializeField]
        private TextAsset _file;

        private void Awake()
        {
            RunEvents();
        }

        public void RunEvents()
        {
            EventsSequenceParser parser = new EventsSequenceParser();
            var sequence = parser.ParseEventsSequence(_file);

            Debug.Log("");
        }
    }
}

