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

        // Character
        ShowAnimation,
        ShowBalloonIcon,

        // ScreenEffects
        FadeScreen,
        FlashScreen,
        ShakeScreen,
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
        SaveBgm,
        ReplayBgm,
        PlayBgs,
        FadeOutBgs,
        PlayMusicalEffect,
        PlaySoundEffect,

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
    }
}
