using NUnit.Framework;
using BattleSystem.UI;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;

namespace Testing.BattleSystem.UI
{
    public class MoveSelectionWindowTests : TestFoundation
    {

        private MoveSelectionWindow CreateMoveSelectionWindow()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            return go.AddComponent<MoveSelectionWindow>();
        }
    }
}
