using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace BattleSystem.Fields
{
    public class BattlefieldsHolder : MonoBehaviour
    {
        [SerializeField]
        private Battlefield[] _fields;

        public Battlefield GetField(int id)
        {
            return _fields.FirstOrDefault(f => f.Id == id);
        }

        public void FeedFields(IEnumerable<Battlefield> fields)
        {
            _fields = new Battlefield[fields.Count()];

            for(int i = 0; i < _fields.Length; i++)
            {
                _fields[i] = fields.ElementAt(i);
            }
        }
    }
}
