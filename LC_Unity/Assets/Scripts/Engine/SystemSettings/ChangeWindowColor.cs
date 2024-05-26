using Engine.Events;
using UnityEngine;

namespace Engine.SystemSettings
{
    public class ChangeWindowColor : IRunnable
    {
        public Color TargetColor { get; set; }

        public void Run()
        {

        }
    }
}
