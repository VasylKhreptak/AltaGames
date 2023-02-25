using System;
using AudioPooler;
using Extensions;
using NaughtyAttributes;
using Physics.Transform.PositionLinker;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Audio
{
    public class PlayableAudio : MonoBehaviour
    {
        [Header("Audios")]
        [SerializeField] private Transform _transform;
        [SerializeField] private AudioClip[] _audioClips;

        [Header("Preferences")]
        [SerializeField] private AudioMixerGroup _output;
        [SerializeField] private bool _playOnTransformPosition;
        [SerializeField, HideIf(nameof(_playOnTransformPosition))] private Vector3 _startPosition;
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;
        [SerializeField, Range(0f, 1f)] private float _spatialBlend;
        [SerializeField, Range(0, 128)] private int _priority = 128;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _useLinker;
        [SerializeField, ShowIf(nameof(_useLinker))] private PositionLinkerData _linkerData;

        private AudioPooler.AudioPooler _audioPooler;

        private int _currentAudioID;

        private AudioPoolItem _currentAudioItem;

        public event Action onPlay;

        [Inject]
        private void Construct(AudioPooler.AudioPooler audioPooler)
        {
            _audioPooler = audioPooler;
        }

        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<Transform>();
        }

        #endregion
        
        public void Play()
        {
            if (IsPlayingLooped()) return;

            _currentAudioID = _audioPooler.PlaySound(_output, _audioClips.Random(), _playOnTransformPosition ? _transform.position : _startPosition,
                _volume, _spatialBlend, _loop, _useLinker ? _linkerData : null, _priority);

            _currentAudioItem = _audioPooler.GetAudioPoolItem(_currentAudioID);

            onPlay?.Invoke();
        }

        public void Stop()
        {
            if (_currentAudioItem == null) return;

            _audioPooler.StopSound(_currentAudioID);

            _currentAudioItem = null;
            _currentAudioID = -1;
        }

        private bool IsPlaying()
        {
            return _currentAudioItem != null && _currentAudioItem.audioSource.isPlaying;
        }

        private bool IsPlayingLooped()
        {
            return IsPlaying() && _loop;
        }
    }
}
