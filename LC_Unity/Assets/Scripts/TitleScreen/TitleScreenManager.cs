using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Party;

namespace TitleScreen
{
    public class TitleScreenManager : MonoBehaviour
    {
        private const float LOAD_DELAY = 1.0f;

        [SerializeField]
        private TextAsset _charactersData;

        private void Start()
        {
            PartyManager.Instance.LoadPartyFromBaseFile(_charactersData);

            StartCoroutine(LoadNextScene());
        }

        private IEnumerator LoadNextScene()
        {
            yield return new WaitForSeconds(LOAD_DELAY);
            SceneManager.LoadScene("Battle");
        }
    }
}
