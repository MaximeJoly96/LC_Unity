using Engine.FlowControl;
using GameProgression;
using System;
using UnityEngine;
using Logging;

namespace FlowControl
{
    public class ConditionEvaluator
    {
        private static ConditionEvaluator _instance;

        public static ConditionEvaluator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConditionEvaluator();

                return _instance;
            }
        }

        private ConditionEvaluator() { }

        #region Main methods
        public bool EvaluateSwitchCondition(SwitchCondition condition)
        {
            PersistentDataHolder dataHolder = PersistentDataHolder.Instance;

            // first we check if the first member is a bool
            if (bool.TryParse(condition.FirstMember, out bool firstMember))
            {
                return EvaluateMembers(dataHolder, firstMember, condition.SecondMember, condition.Condition);
            } 
            // if not, we check if the second one is a bool
            else if(bool.TryParse(condition.SecondMember, out bool secondMember))
            {
                return EvaluateMembers(dataHolder, secondMember, condition.FirstMember, condition.Condition);
            }
            // else we assume none of them is a bool
            else
            {
                return EvaluateMembers(dataHolder, condition);
            }
        }

        public bool EvaluateVariableCondition(VariableCondition condition)
        {
            PersistentDataHolder dataHolder = PersistentDataHolder.Instance;

            // first we check if the first member is a int
            if(int.TryParse(condition.FirstMember, out int firstMember))
            {
                return EvaluateMembers(dataHolder, firstMember, condition.SecondMember, condition.Condition);
            }
            // else we check if the second member is a int
            else if(int.TryParse(condition.SecondMember, out int secondMember))
            {
                return EvaluateMembers(dataHolder, secondMember, condition.FirstMember, condition.Condition);
            }
            // else we assume they are both variables
            else
            {
                return EvaluateMembers(dataHolder, condition);
            }
        }

        public bool EvaluateTimerCondition(TimerCondition condition)
        {
            TimersManager timersManager = UnityEngine.Object.FindObjectOfType<TimersManager>();

            // Is our first member a duration ?
            if(int.TryParse(condition.FirstMember, out int firstMember))
            {
                return EvaluateMembers(timersManager, firstMember, condition.SecondMember, condition.Condition);
            }
            // If not, then is the second member a duration ?
            else if(int.TryParse(condition.SecondMember, out int secondMember))
            {
                return EvaluateMembers(timersManager, secondMember, condition.FirstMember, condition.Condition);
            }
            // Else, it means both members are existing timers
            else
            {
                return EvaluateMembers(timersManager, condition);
            }
        }
        #endregion

        #region Process Condition
        private static bool ProcessSwitchCondition(bool v1, bool v2, SwitchCondition.Type condition)
        {
            return condition switch
            {
                SwitchCondition.Type.Equal => v1 == v2,
                SwitchCondition.Type.NotEqual => v1 != v2,
                _ => throw new InvalidOperationException("Provided condition does not exist for SwitchCondition : " + condition.ToString()),
            };
        }

        private static bool ProcessTimerCondition(Timer timer, int threshold, TimerCondition.Type condition)
        {
            return condition switch
            {
                TimerCondition.Type.After => timer.CurrentTime > threshold,
                TimerCondition.Type.Before => timer.CurrentTime < threshold,
                _ => throw new InvalidOperationException("Provided condition does not exist for TimerCondition : " + condition.ToString()),
            };
        }

        private static bool ProcessTimerCondition(Timer t1, Timer t2, TimerCondition.Type condition)
        {
            return condition switch
            {
                TimerCondition.Type.After => t1.CurrentTime > t2.CurrentTime,
                TimerCondition.Type.Before => t1.CurrentTime < t2.CurrentTime,
                _ => throw new InvalidOperationException("Provided condition does not exist for TimerCondition : " + condition.ToString()),
            };
        }

        private static bool ProcessVariableCondition(int v1, int v2, VariableCondition.Type condition)
        {
            return condition switch
            {
                VariableCondition.Type.Equal => v1 == v2,
                VariableCondition.Type.Different => v1 != v2,
                VariableCondition.Type.EqualOrGreaterThan => v1 >= v2,
                VariableCondition.Type.EqualOrSmallerThan => v1 <= v2,
                VariableCondition.Type.GreaterThan => v1 > v2,
                VariableCondition.Type.SmallerThan => v1 < v2,
                _ => throw new InvalidOperationException("Provided condition does not exist for VariableCondition : " + condition.ToString()),
            };
        }
        #endregion

        #region Evaluate Switch
        private bool EvaluateMembers(PersistentDataHolder dataHolder, bool checkedValue, string member, SwitchCondition.Type condition)
        {
            if (dataHolder.HasKey(member))
            {
                try
                {
                    bool value = (bool)dataHolder.GetData(member);
                    return ProcessSwitchCondition(checkedValue, value, condition);
                }
                catch (InvalidCastException)
                {
                    LogsHandler.Instance.LogError("Provided member is not a bool " + member + " but you are using it in SwitchCondition.");
                }
            }
            else
            {
                LogsHandler.Instance.LogError("Cannot evaluate switch condition because key " + member + " does not exist.");
            }

            return false;
        }

        private bool EvaluateMembers(PersistentDataHolder dataHolder, SwitchCondition condition)
        {
            if (dataHolder.HasKey(condition.FirstMember) && dataHolder.HasKey(condition.SecondMember))
            {
                try
                {
                    bool v1 = (bool)dataHolder.GetData(condition.FirstMember);
                    bool v2 = (bool)dataHolder.GetData(condition.SecondMember);
                    return ProcessSwitchCondition(v1, v2, condition.Condition);
                }
                catch (InvalidCastException)
                {
                    LogsHandler.Instance.LogError("One of the provided members (" + condition.FirstMember + " or "
                                                  + condition.SecondMember + " or both) is not a bool but you are using it in SwitchCondition.");
                }
            }
            else
            {
                LogsHandler.Instance.LogError("One of the provided members (" + condition.FirstMember + " or " +
                                              condition.SecondMember + " or both) does not exist.");
            }

            return false;
        }
        #endregion

        #region Evaluate Timer
        private bool EvaluateMembers(TimersManager timersManager, int threshold, string member, TimerCondition.Type condition)
        {
            if (timersManager.HasTimer(member))
            {
                Timer timer = timersManager.GetTimer(member);
                return ProcessTimerCondition(timer, threshold, condition);
            }
            else
            {
                LogsHandler.Instance.LogError("Timer " + member + " does not exist but you are trying to use it in TimerCondition.");
            }

            return false;
        }

        private bool EvaluateMembers(TimersManager timersManager, TimerCondition condition)
        {
            if (timersManager.HasTimer(condition.FirstMember) &&
                timersManager.HasTimer(condition.SecondMember))
            {
                return ProcessTimerCondition(timersManager.GetTimer(condition.FirstMember),
                                             timersManager.GetTimer(condition.SecondMember),
                                             condition.Condition);
            }
            else
            {
                LogsHandler.Instance.LogError("One of the provided member (" + condition.FirstMember + " or "
                                              + condition.SecondMember + " or both) does not exist.");
            }

            return false;
        }
        #endregion

        #region Evaluate Variable
        private static bool EvaluateMembers(PersistentDataHolder dataHolder, int targetValue, string member, VariableCondition.Type condition)
        {
            if (dataHolder.HasKey(member))
            {
                try
                {
                    int value = (int)dataHolder.GetData(member);
                    return ProcessVariableCondition(targetValue, value, condition);
                }
                catch (InvalidCastException)
                {
                    LogsHandler.Instance.LogError("You are trying to use " + member
                                                  + " as an integer in a VariableCondition but it was not stored as such.");
                }
            }
            else
            {
                LogsHandler.Instance.LogError("Provided variable " + member + " does not exist.");
            }

            return false;
        }

        private static bool EvaluateMembers(PersistentDataHolder dataHolder, VariableCondition condition)
        {
            if(dataHolder.HasKey(condition.FirstMember) &&
               dataHolder.HasKey(condition.SecondMember))
            {
                try
                {
                    int v1 = (int)dataHolder.GetData(condition.FirstMember);
                    int v2 = (int)dataHolder.GetData(condition.SecondMember);

                    return ProcessVariableCondition(v1, v2, condition.Condition);
                }
                catch(InvalidCastException)
                {
                    LogsHandler.Instance.LogError("One of the provided members (" + condition.FirstMember + " or "
                                                  + condition.SecondMember + " or both) is not a int but you are using it in VariableCondition.");
                }
            }
            else
            {
                LogsHandler.Instance.LogError("One of the provided members (" + condition.FirstMember + " or " +
                                                   condition.SecondMember + " or both) does not exist.");
            }

            return false;
        }
        #endregion
    }
}
