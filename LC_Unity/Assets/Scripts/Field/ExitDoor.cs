using Engine.MusicAndSounds;
using Movement;
using MusicAndSounds;
using System.Collections;
using UnityEngine;

namespace Field
{
    public class ExitDoor : Door
    {
        public override void RunSequence()
        {
            StartCoroutine(Exit());
        }

        private IEnumerator Exit()
        {
            FindObjectOfType<AudioPlayer>().PlaySoundEffect(new PlaySoundEffect
            {
                Name = "OpenWoodenDoor",
                Volume = 0.25f,
                Pitch = 1.0f
            });

            yield return null;

            _interior.gameObject.SetActive(false);
            FindObjectOfType<FieldBuilder>().SwitchToInteriorMode(false);
            PlayerController pc = FindObjectOfType<PlayerController>();

            pc.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            pc.transform.Translate(new Vector3(0.0f, -0.25f));

            FinishedSequence.Invoke();
            DoorStatusChanged.Invoke(false);
        }

    }
}
