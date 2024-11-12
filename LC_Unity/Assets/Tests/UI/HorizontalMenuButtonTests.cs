using UI;
using UnityEngine;
using NUnit.Framework;
using System.Linq;
using UnityEngine.TestTools;
using System.Collections;

namespace Testing.UI
{
    public class HorizontalMenuButtonTests : TestFoundation
    {
        [UnityTest]
        public IEnumerator ButtonCanBeHovered()
        {
            HorizontalMenuButton button = ComponentCreator.CreateHorizontalMenuButton();
            _usedGameObjects.Add(button.gameObject);
            button.HoverButton(true);

            yield return null;

            Animator animator = button.GetComponent<Animator>();
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            AnimationClip clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name.Contains("Hover"));                              
            
            Assert.AreEqual(Animator.StringToHash(clip.name), stateInfo.shortNameHash);

            button.HoverButton(false);

            yield return null;

            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            clip = animator.runtimeAnimatorController.animationClips.FirstOrDefault(c => c.name.Contains("Idle"));

            Assert.AreEqual(Animator.StringToHash(clip.name), stateInfo.shortNameHash);
        }
    }
}
