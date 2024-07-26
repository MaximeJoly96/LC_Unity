using System.Collections.Generic;

namespace BattleSystem.Model
{
    public class TimelineSegment
    {
        public int Priority { get; set; }
        public List<TimelineAction> Actions { get; set; }
        public float Length
        {
            get
            {
                float maxLength = 0.0f;

                for(int i =  0; i < Actions.Count; i++)
                {
                    float length = Actions[i].Length + Actions[i].StartPoint;
                    if(maxLength < length)
                        maxLength = length;
                }

                return maxLength;
            }
        }
    }
}
