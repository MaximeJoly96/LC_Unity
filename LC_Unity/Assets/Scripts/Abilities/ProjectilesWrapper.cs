using UnityEngine;
using System.Linq;

namespace Abilities
{
    public class ProjectilesWrapper : MonoBehaviour
    {
        [SerializeField]
        private AbilityProjectile[] _projectiles;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public AbilityProjectile GetProjectileById(int id)
        {
            return _projectiles.FirstOrDefault(p => p.Id == id);
        }
    }
}
