using NUnit.Framework;
using Field.Routines;
using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;
using Field;
using Movement;
using Timing;

namespace Testing.Field.Routines
{
    public class RoutineRunnerTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator ProvidedRoutineCanRun()
        {
            Waiter waiter = CreateWaiter();
            RoutineRunner runner = CreateRunner("Assets/Tests/Field/Routines/TestData/TestRoutine.xml",
                                                "Assets/Tests/Field/Routines/TestAnimations/Test_RoutineAnimationController.controller");

            yield return null;

            Assert.IsTrue(runner.GetComponent<Collider2D>().isTrigger);

            Vector2 targetPosition = (runner.transform.position + new Vector3(-0.8f, 0.0f));

            yield return new WaitUntil(() => Vector2.Distance(targetPosition, runner.transform.position) < 0.05f);
            yield return null;

            Assert.AreEqual(Direction.Down, runner.Agent.CurrentDirection);

            targetPosition = (runner.transform.position + new Vector3(1.6f, 0.0f));
            yield return new WaitUntil(() => Vector2.Distance(targetPosition, runner.transform.position) < 0.05f);
            yield return null;

            Assert.AreEqual(Direction.Down, runner.Agent.CurrentDirection);
        }

        [UnityTest]
        public IEnumerator RoutineCanBeInterruptedAndResumed()
        {
            Waiter waiter = CreateWaiter();
            RoutineRunner runner = CreateRunner("Assets/Tests/Field/Routines/TestData/TestRoutine.xml",
                                                "Assets/Tests/Field/Routines/TestAnimations/Test_RoutineAnimationController.controller");

            yield return null;

            Assert.IsTrue(runner.GetComponent<Collider2D>().isTrigger);

            Vector2 targetPosition = (runner.transform.position + new Vector3(-0.8f, 0.0f));

            yield return new WaitUntil(() => Vector2.Distance(targetPosition, runner.transform.position) < 0.05f);
            yield return new WaitForSeconds(0.5f);

            runner.Interrupt();

            yield return new WaitForSeconds(5.0f);

            Assert.AreEqual(Direction.Down, runner.Agent.CurrentDirection);

            yield return new WaitForSeconds(1.0f);

            Assert.AreEqual(Direction.Down, runner.Agent.CurrentDirection);

            runner.Resume();

            targetPosition = (runner.transform.position + new Vector3(1.6f, 0.0f));
            yield return new WaitUntil(() => Vector2.Distance(targetPosition, runner.transform.position) < 0.05f);
            yield return null;

            Assert.AreEqual(Direction.Down, runner.Agent.CurrentDirection);
        }

        private RoutineRunner CreateRunner(string routinePath, string animatorPath)
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            RoutineRunner runner = go.AddComponent<RoutineRunner>();
            runner.RoutineData = AssetDatabase.LoadAssetAtPath<TextAsset>(routinePath);

            BoxCollider2D collider = go.AddComponent<BoxCollider2D>();

            Agent agent = go.AddComponent<Agent>();
            agent.UpdateSpeed(2.0f);

            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>(animatorPath);

            return runner;
        }

        private Waiter CreateWaiter()
        {
            GameObject go = ComponentCreator.CreateEmptyGameObject();
            _usedGameObjects.Add(go);

            return go.AddComponent<Waiter>();
        }
    }
}
