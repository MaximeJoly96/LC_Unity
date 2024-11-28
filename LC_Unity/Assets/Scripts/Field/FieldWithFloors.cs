using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class FieldWithFloors : PlayableField
    {
        [SerializeField]
        private List<Transform> _floors;

        public int CurrentFloorIndex { get; private set; }

        public void ChangeFloor(bool up)
        {
            UpdateIndex(up);
            UpdateVisuals();
        }

        public void AddFloor(Transform floor)
        {
            if(_floors == null)
                _floors = new List<Transform>();

            _floors.Add(floor);
        }

        public void RemoveFloor(int index)
        {
            if( _floors != null)
                _floors.RemoveAt(index);
        }

        public void SetFloor(int id)
        {
            CurrentFloorIndex = Mathf.Clamp(id, 0, _floors.Count - 1);
        }

        private void UpdateIndex(bool up)
        {
            if (up && CurrentFloorIndex < _floors.Count - 1)
                CurrentFloorIndex++;
            else if (!up && CurrentFloorIndex > 0)
                CurrentFloorIndex--;
        }

        public void UpdateVisuals()
        {
            for(int i = 0; i < _floors.Count; i++)
            {
                _floors[i].gameObject.SetActive(i == CurrentFloorIndex);
            }
        }

        private void OnEnable()
        {
            UpdateVisuals();
        }
    }
}
