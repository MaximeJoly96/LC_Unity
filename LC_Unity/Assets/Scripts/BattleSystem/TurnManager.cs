using System.Collections.Generic;

namespace BattleSystem
{
    public class TurnManager
    {
        private List<BattlerBehaviour> _characters;
        private int _currentCharacterCursor;

        public BattlerBehaviour CurrentCharacter { get { return _characters[_currentCharacterCursor]; } }

        public TurnManager(List<BattlerBehaviour> characters)
        {
            _characters = characters;
        }

        public BattlerBehaviour SwitchToNextCharacter()
        {
            if(_currentCharacterCursor < _characters.Count - 1)
                _currentCharacterCursor++;
            return CurrentCharacter;
        }
    }
}
