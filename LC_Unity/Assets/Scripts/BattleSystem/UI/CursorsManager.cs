using UnityEngine;
using System.Collections.Generic;

namespace BattleSystem.UI
{
    public class CursorsManager : MonoBehaviour
    {
        [SerializeField]
        private BattleCursor _cursorPrefab;

        private BattleCursor _currentCursor;
        private List<BattleCursor> _allCursors;

        private List<BattleCursor> AllCursors
        {
            get
            {
                if (_allCursors == null)
                    _allCursors = new List<BattleCursor>();

                return _allCursors;
            }
        }


        public void CreateCursor(Vector3 position)
        {
            _currentCursor = Instantiate(_cursorPrefab, position, Quaternion.identity);

            AllCursors.Add(_currentCursor);
        }

        public void StopCurrentCursor() 
        {
            _currentCursor.Stop();
        }

        public void ClearCursors()
        {
            for(int i = 0; i < AllCursors.Count; i++)
            {
                Destroy(AllCursors[i].gameObject);
            }

            AllCursors.Clear();
            _currentCursor = null;
        }

        public void UpdateCurrentCursor(Vector3 position)
        {
            _currentCursor.transform.position = position;
        }
    }
}
