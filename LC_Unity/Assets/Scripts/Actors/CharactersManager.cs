using System.Collections.Generic;
using System.Linq;
using Engine.Actor;
using UnityEngine;

namespace Actors
{
    public class CharactersManager
    {
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

        public List<Character> Characters { get; private set; }

        private CharactersManager()
        {
            Characters = new List<Character>();
        }

        public Character GetCharacter(int id)
        {
            return Characters.FirstOrDefault(c => c.Id == id);
        }

        public void AddCharacter(Character character)
        {
            Characters.Add(character);
        }

        public void LoadCharactersFromFile(TextAsset file)
        {
            Characters.AddRange(XmlCharacterParser.ParseCharacters(file));
        }
    }
}
