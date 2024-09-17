using System;
using UnityEngine;
using System.Linq;
using Engine.Character;

namespace Utils
{
    [Serializable]
    public class BalloonIconDisplay
    {
        public ShowBalloonIcon.BalloonType Type;
        public Transform BalloonIcon;
    }

    public class BalloonIconsWrapper : IconsWrapper
    {
        [SerializeField]
        private BalloonIconDisplay[] _icons;

        public BalloonIconDisplay GetBalloonIcon(ShowBalloonIcon.BalloonType type)
        {
            return _icons.FirstOrDefault(x => x.Type == type);
        }
    }
}
