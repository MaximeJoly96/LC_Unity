using UnityEngine;

namespace BattleSystem.Model
{
    public class TimelineAction
    {
        public float Length { get; private set; }
        public float StartPoint { get; private set; }
        public int Priority { get; set; }

        public TimelineAction(BattlerBehaviour battler)
        {
            Length = ComputeActionLength(battler);
            StartPoint = ComputeActionStartPoint(battler);
        }

        public TimelineAction(float length, float startPoint, int priority)
        {
            Length = length;
            StartPoint = startPoint;
            Priority = priority;
        }

        public float ComputeActionStartPoint(BattlerBehaviour battler)
        {
            // Within a certain actionPriority, we resolve actions from the highest agility to the lowest
            // the timing within this priority is determined by the proportion of the casterAgility based on
            // the min and max agility
            // e.g.: we have 3 battlers with the following agility stats : battler1 : 300, battler2 : 150, battler3 : 100
            // assuming they all cast an action priority 0, the timing distribution is the following :
            // battler1 starts its action at moment T1, while battler3 starts its action at moment T3
            // in our context, T1 = 0 and T3 = 200, so T2 = 150
            // T1             T2  T3
            // |              |   |
            // --------------------      ---> for ref, there are 20 hyphens here

            return 0.0f;
        }

        public float ComputeActionLength(BattlerBehaviour battler)
        {
            float maxDistance = 0.0f;

            for (int i = 0; i < battler.LockedInAbility.Targets.Count; i++)
            {
                float distance = Vector2.Distance(battler.transform.position,
                                                  battler.LockedInAbility.Targets[i].transform.position);

                if (distance > maxDistance)
                    maxDistance = distance;
            }

            // Each action has a baseLength, which is tied to the length of the animation.
            // Then, we need to compute the travel time, which is based on the distance and the movement speed
            // the movement speed can either be from the projectile itself OR the caster who has to move
            // the speed is assumed to be linear here. This is a projection for the timeline, so movespeed
            // modifiers applied during the resolve phase are not reflected here.
            // NOTE TO MY FUTURE SELF :
            // If I want to implement non-linear movement speed, I should create a sequence of timestamp+movespeed objects

            return 0.0f + (maxDistance / 1.0f);
        }
    }
}
