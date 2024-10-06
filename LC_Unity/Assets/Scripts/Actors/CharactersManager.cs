using System.Collections.Generic;
using System.Linq;
using Engine.Actor;

namespace Actors
{
    public class CharactersManager
    {
        private readonly List<Character> _characters;

        private static CharactersManager _instance;

        public static CharactersManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CharactersManager();

                return _instance;
            }
        }

        private CharactersManager()
        {
            _characters = new List<Character>();
        }

        public void ChangeCharacterName(ChangeName change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.UpdateName(change.Value);
        }

        public void ChangeCharacterLevel(ChangeLevel change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.ChangeLevel(change.Amount);
        }

        public void ChangeCharacterExp(ChangeExp change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.GiveExp(change.Amount);
        }

        public void RecoverAll(RecoverAll recoverAll)
        {
            for(int i = 0; i < _characters.Count; i++)
            {
                _characters[i].Recover();
            }
        }

        public void ChangeEquipment(ChangeEquipment change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
                character.ChangeEquipment(change.ItemId);
        }

        public void ChangeSkills(ChangeSkills change)
        {
            Character character = GetCharacter(change.CharacterId);

            if (character != null)
            {
                switch(change.Action)
                {
                    case Engine.Actor.ChangeSkills.ActionType.Forget:
                        character.ForgetSkill(change.SkillId);
                        break;
                    case Engine.Actor.ChangeSkills.ActionType.Learn:
                        character.ForgetSkill(change.SkillId);
                        break;
                }
            }
        }

        public Character GetCharacter(int id)
        {
            return _characters.FirstOrDefault(c => c.Id == id);
        }
    }
}
