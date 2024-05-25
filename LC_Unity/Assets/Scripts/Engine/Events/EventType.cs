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
        ChangePartyMember
    }
}
