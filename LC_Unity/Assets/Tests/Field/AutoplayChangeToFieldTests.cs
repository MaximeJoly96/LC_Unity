using Core;
using Engine.Events;
using Field;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Testing.Field
{
    public class AutoplayChangeToFieldTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator GlobalStateChangesToOnFieldAfterInstantiation()
        {
            GlobalStateMachine.Instance.UpdateState(GlobalStateMachine.State.BrowsingQuests);

            AutoplayChangeToField auto = CreateAutoplay();

            yield return null;

            Assert.AreEqual(GlobalStateMachine.State.OnField, GlobalStateMachine.Instance.CurrentState);
        }

        private AutoplayChangeToField CreateAutoplay()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            go.AddComponent<EventsRunner>();
            return go.AddComponent<AutoplayChangeToField>();
        }
    }
}
