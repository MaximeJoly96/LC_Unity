using UnityEngine;

namespace Utils
{
    public class IconsWrapper : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
