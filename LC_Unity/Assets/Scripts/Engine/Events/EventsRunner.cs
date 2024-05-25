using UnityEngine;
using System.Collections.Generic;
using Engine.Message;

namespace Engine.Events
{
    public class EventsRunner : MonoBehaviour
    {
        [SerializeField]
        private List<IRunnable> _steps;

        [SerializeField]
        private TextAsset _test;

        private void Awake()
        {
            RunEvents();
        }

        public void RunEvents()
        {
            XmlDialogParser parser = new XmlDialogParser();
            var dialog = parser.ParseDialogData(_test);

            Debug.Log("");
        }
    }
}

