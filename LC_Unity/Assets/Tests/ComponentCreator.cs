﻿using UnityEditor;
using UnityEngine;
using UI;
using Inputs;
using MusicAndSounds;
using Questing;
using Inventory;
using Menus.SubMenus.Quests;
using TMPro;
using Language;
using UnityEngine.UI;

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

        public static QuestsHorizontalMenuButton CreateQuestsHorizontalMenuButton(QuestStatus status)
        {
            GameObject go = CreateEmptyGameObject();
            QuestsHorizontalMenuButton button = go.AddComponent<QuestsHorizontalMenuButton>();
            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = CreateAnimatorController("UI/TestAnimations/HorizontalMenuButton/HorizontalMenuButtonController.controller");

            button.StatusType = status;
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

            TextMeshProUGUI label = CreateText();
            label.transform.SetParent(item.transform);
            item.Label = label;

            return item;
        }

        public static SelectableQuest CreateSelectableQuest()
        {
            GameObject go = CreateEmptyGameObject();
            SelectableQuest item = go.AddComponent<SelectableQuest>();

            GameObject cursor = CreateEmptyGameObject();
            cursor.transform.SetParent(item.transform);
            item.Cursor = item.transform.GetChild(0);

            TextMeshProUGUI label = CreateText();
            label.transform.SetParent(item.transform);
            item.Label = label;

            return item;
        }

        public static TextMeshProUGUI CreateText()
        {
            GameObject go = CreateEmptyGameObject();
            return go.AddComponent<TextMeshProUGUI>();
        }

        public static Image CreateImage()
        {
            GameObject go = CreateEmptyGameObject();
            return go.AddComponent<Image>();
        }

        public static Localizer CreateLocalizer(string languageFilePath, global::Language.Language language)
        {
            GameObject localizer = new GameObject("Localizer");
            Localizer component = localizer.AddComponent<Localizer>();

            TextAsset[] files = new TextAsset[] { AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Tests/" + languageFilePath) };
            component.LoadLanguage(language, files);

            return component;
        }

        public static RewardComponentDisplay CreateRewardComponentDisplay()
        {
            GameObject go = CreateEmptyGameObject();
            RewardComponentDisplay reward = go.AddComponent<RewardComponentDisplay>();

            TextMeshProUGUI label = CreateText();
            label.transform.SetParent(reward.transform);
            reward.Label = label;

            TextMeshProUGUI quantity = CreateText();
            quantity.transform.SetParent(reward.transform);
            reward.Quantity = quantity;

            Image icon = CreateImage();
            icon.transform.SetParent(reward.transform);
            reward.Icon = icon;

            return reward;
        }
    }
}
