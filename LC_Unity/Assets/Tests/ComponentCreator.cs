using UnityEditor;
using UnityEngine;
using UI;
using Inputs;
using MusicAndSounds;
using Questing;
using Inventory;

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

        public static QuestsWrapper CreateQuestsWrapper(string mainQuestsPath, string sideQuestsPath, string bountiesPath, string proQuestsPath)
        {
            GameObject go = CreateEmptyGameObject();
            QuestsWrapper wrapper = go.AddComponent<QuestsWrapper>();

            wrapper.Feed(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/" + mainQuestsPath),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/" + sideQuestsPath),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/" + bountiesPath),
                         AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/" + proQuestsPath));

            return wrapper;
        }

        public static ItemsWrapper CreateItemsWrapper(string itemsPath)
        {
            GameObject go = CreateEmptyGameObject();
            ItemsWrapper wrapper = go.AddComponent<ItemsWrapper>();

            wrapper.FeedConsumables(AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/" + itemsPath));
            return wrapper;
        }

        public static SelectableItem CreateSelectableItem()
        {
            GameObject go = CreateEmptyGameObject();
            SelectableItem item = go.AddComponent<SelectableItem>();

            GameObject cursor = CreateEmptyGameObject();
            cursor.transform.SetParent(item.transform);
            item.Cursor = item.transform.GetChild(0);

            return item;
        }
    }
}
