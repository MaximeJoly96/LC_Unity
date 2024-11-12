using UnityEditor;
using UnityEngine;
using UI;
using Inputs;
using MusicAndSounds;

namespace Testing
{
    public static class ComponentCreator
    {
        public static GameObject CreateEmptyGameObject()
        {
            return CreateEmptyGameObject(string.Empty);
        }

        public static GameObject CreateEmptyGameObject(string name)
        {
            return new GameObject(name);
        }

        public static HorizontalMenuButton CreateHorizontalMenuButton()
        {
            GameObject go = CreateEmptyGameObject();
            HorizontalMenuButton button = go.AddComponent<HorizontalMenuButton>();
            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = CreateAnimatorController("UI/TestAnimations/HorizontalMenuButton/HorizontalMenuButtonController.controller");

            return button;
        }

        public static RuntimeAnimatorController CreateAnimatorController(string path)
        {
            return AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Tests/" + path);
        }

        public static InputController CreateInputController()
        {
            GameObject go = CreateEmptyGameObject();
            return go.AddComponent<InputController>();
        }

        public static AudioPlayer CreateAudioPlayer()
        {
            GameObject go = CreateEmptyGameObject();

            return go.AddComponent<AudioPlayer>();
        }
    }
}
