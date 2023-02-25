using Audio;
using UnityEngine;
using Action = CBA.Actions.Core.Action;

namespace Actions.Audio
{
    public class PlayAudio : Action
    {
        [Header("References")]
        [SerializeField] private PlayableAudio _playableAudio;

        #region MonoBehaviour

        private void OnValidate()
        {
            _playableAudio ??= GetComponent<PlayableAudio>();
        }

        #endregion

        public override void Do()
        {
            _playableAudio.Play();
        }
    }
}
