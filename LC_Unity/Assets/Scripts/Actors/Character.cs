using Logging;

namespace Actors
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Exp { get; private set; }


        public Character() : this(0, "Test")
        {

        }

        public Character(int id, string name)
        {
            Id = id;
            Name = name;
            Exp = 0;
        }

        public void ChangeLevel(int amount)
        {
            LogsHandler.Instance.LogWarning("ChangeLevel has not been implemented yet.");
        }

        public void GiveExp(int amount)
        {
            Exp += amount;
        }

        public void Recover()
        {
            LogsHandler.Instance.LogWarning("Recover has not been implemented yet.");
        }

        public void ChangeEquipment(int itemId)
        {
            LogsHandler.Instance.LogWarning("ChangeEquipment has not been implemented yet.");
        }

        public void LearnSkill(int skillId)
        {
            LogsHandler.Instance.LogWarning("LearnSkill has not been implemented yet.");
        }

        public void ForgetSkill(int skillId)
        {
            LogsHandler.Instance.LogWarning("ForgetSkill has not been implemented yet.");
        }
    }
}
