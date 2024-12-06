namespace Engine.Events
{
    public enum EventType
    {
        // Message
        DisplayDialog,
        DisplayChoice,
        InputNumber,

        // GameProgression
        ControlSwitch,
        ControlVariable,
        ControlTimer,

        // FlowControl
        ConditionalBranch,
        InventoryCondition,
        QuestCondition,

        // Party
        ChangeGold,
        ChangeItems,
        ChangePartyMember,

        // Actor
        ChangeEquipment,
        ChangeExp,
        ChangeLevel,
        ChangeName,
        ChangeSkills,
        RecoverAll,

        // Movement
        GetOnOffVehicle,
        ScrollMap,
        SetMoveRoute,
        TransferObject,
        CameraFollowPlayer,
        ChangeFloor,
        SetFloor,

        // Character
        ShowAnimation,
        ShowBalloonIcon,
        ShowAgentAnimation,
        DisableAgent,
        EnableAgent,

        // ScreenEffects
        FadeScreen,
        FlashScreen,
        TintScreen,

        // Timing
        Wait,

        // PictureAndWeather
        MovePicture,
        RotatePicture,
        SetWeatherEffects,
        ShowPicture,
        TintPicture,

        // MusicAndSounds
        PlayBgm,
        FadeOutBgm,
        PlayBgs,
        FadeOutBgs,
        PlayMusicalEffect,
        PlaySoundEffect,
        StopAllAudio,

        // SceneControl
        BattleProcessing,
        ShopProcessing,
        NameInputProcessing,
        OpenMenu,
        OpenSave,
        GameOver,
        ReturnToTitle,

        // SystemSettings
        ChangeBattleBgm,
        ChangeBattleEndMusicalEffect,
        ChangeMenuAccess,
        ChangeSaveAccess,
        ChangeEncounterAccess,
        ChangeFormationAccess,
        ChangeWindowColor,
        ChangeActorGraphic,
        AllowCutsceneSkip,
        ChangeGameState,

        // Map
        ChangeMapNameDisplay,

        // Questing
        StartQuest,
        FailQuest,
        ProgressQuest,
        CompleteQuest
    }
}
