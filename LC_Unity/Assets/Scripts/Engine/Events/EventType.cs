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
        TransferObject
    }
}
