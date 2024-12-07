using UnityEditor;
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
using Save;
using Engine.Events;
using Movement;
using BattleSystem.UI;
using Core.Model;
using Utils;
using Actors;
using BattleSystem;

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

        public static QuestDetailsDisplay CreateQuestDetailsDisplay()
        {
            GameObject go = CreateEmptyGameObject();
            QuestDetailsDisplay display = go.AddComponent<QuestDetailsDisplay>();

            TextMeshProUGUI nameLabel = CreateText();
            nameLabel.transform.SetParent(display.transform);
            display.NameLabel = nameLabel;

            TextMeshProUGUI descriptionLabel = CreateText();
            descriptionLabel.transform.SetParent(display.transform);
            display.DescriptionLabel = descriptionLabel;

            GameObject stepsDisplayGo = CreateEmptyGameObject();
            QuestStepsDisplay stepsDisplay = stepsDisplayGo.AddComponent<QuestStepsDisplay>();
            stepsDisplay.transform.SetParent(display.transform);
            display.StepsDisplay = stepsDisplay;

            GameObject rewardsDisplayGo = CreateEmptyGameObject();
            QuestRewardDisplay rewardsDisplay = rewardsDisplayGo.AddComponent<QuestRewardDisplay>();
            rewardsDisplay.transform.SetParent(display.transform);
            display.RewardsDisplay = rewardsDisplay;

            return display;
        }

        public static IndividualQuestStepDisplay CreateQuestStepDisplay()
        {
            GameObject go = CreateEmptyGameObject();
            IndividualQuestStepDisplay stepDisplay = go.AddComponent<IndividualQuestStepDisplay>();

            TextMeshProUGUI nameLabel = CreateText();
            nameLabel.transform.SetParent(stepDisplay.transform);
            stepDisplay.StepLabel = nameLabel;

            TextMeshProUGUI descriptionLabel = CreateText();
            descriptionLabel.transform.SetParent(stepDisplay.transform);
            stepDisplay.StepDescription = descriptionLabel;

            GameObject rewardDisplayGo = CreateEmptyGameObject();
            QuestRewardDisplay rewardDisplay = rewardDisplayGo.AddComponent<QuestRewardDisplay>();
            rewardDisplay.SetRewardComponentPrefab(CreateRewardComponentDisplay());
            rewardDisplay.transform.SetParent(stepDisplay.transform);
            stepDisplay.RewardDisplay = rewardDisplay;

            return stepDisplay;
        }

        public static SaveSlot CreateSaveSlot()
        {
            GameObject slotGo = CreateEmptyGameObject();
            SaveSlot slot = slotGo.AddComponent<SaveSlot>();

            TextMeshProUGUI inGameTime = CreateText();
            inGameTime.transform.SetParent(slotGo.transform);
            slot.InGameTime = inGameTime;

            TextMeshProUGUI location = CreateText();
            location.transform.SetParent(slotGo.transform);
            slot.Label = location;

            TextMeshProUGUI slotIdLabel = CreateText();
            slotIdLabel.transform.SetParent(slotGo.transform);
            slot.SlotIdLabel = slotIdLabel;

            Image cursor = CreateImage();
            cursor.transform.SetParent(slot.transform);
            slot.Cursor = cursor.transform;

            return slot;
        }

        public static EventsRunner CreateEventsRunner()
        {
            GameObject go = CreateEmptyGameObject();
            return go.AddComponent<EventsRunner>();
        }

        public static PlayerController CreatePlayerController()
        {
            GameObject go = CreateEmptyGameObject();
            return go.AddComponent<PlayerController>();
        }

        public static SimpleTextWindow CreateSimpleTextWindow(string animatorPath)
        {
            GameObject go = CreateEmptyGameObject();

            SimpleTextWindow window = go.AddComponent<SimpleTextWindow>();
            Animator animator = go.AddComponent<Animator>();
            animator.runtimeAnimatorController = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/Tests/" + animatorPath);
            TextMeshProUGUI text = go.AddComponent<TextMeshProUGUI>();
            window.Text = text;

            return window;
        }

        public static Character CreateDummyCharacter()
        {
            return new Character(new ElementIdentifier(0, "name", ""),
                                 new QuadraticFunction(10.0f, 10.0f, 10.0f),
                                 new StatScalingFunction(100.0f, 1.0f, 100.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f),
                                 new StatScalingFunction(10.0f, 1.0f, 10.0f));
        }

        public static BattleUiManager CreateBattleUiManager()
        {
            GameObject go = CreateEmptyGameObject();

            SimpleTextWindow helpWindow = CreateSimpleTextWindow("BattleSystem/UI/TestAnimations/SimpleWindowController.controller");
            SimpleTextWindow attackLabelWindow = CreateSimpleTextWindow("BattleSystem/UI/TestAnimations/SimpleWindowController.controller");

            BattleUiManager uiManager = go.AddComponent<BattleUiManager>();
            uiManager.HelpWindow = helpWindow;
            uiManager.AttackLabelWindow = attackLabelWindow;

            PlayerGlobalUi playerGlobalUi = CreatePlayerGlobalUi();
            uiManager.PlayerGlobalUi = playerGlobalUi;

            return uiManager;
        }

        public static PlayerGlobalUi CreatePlayerGlobalUi()
        {
            GameObject go = CreateEmptyGameObject();

            PlayerGlobalUi ui = go.AddComponent<PlayerGlobalUi>();
            return ui;
        }
    }
}
