using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using Physics.Transform.PositionLinker;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;
using Transform = UnityEngine.Transform;

namespace AudioPooler
{
    public class AudioPooler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _transform;

        [Header("Sound Preferences")]
        [SerializeField, Min(0)] private int _soundsCount;
        [SerializeField, Min(0)] private float _minSoundDistance;
        [SerializeField, Min(0)] private float _maxSoundDistance;
        [SerializeField] private AudioRolloffMode _rolloffMode = AudioRolloffMode.Linear;

        [Header("Pool Preferences")]
        [SerializeField] private GameObject _audioItemPrefab;

        private List<AudioPoolItem> _pool = new List<AudioPoolItem>();
        private Dictionary<int, AudioPoolItem> _activePool = new Dictionary<int, AudioPoolItem>();

        private int _idGiver;

        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        #region MonoBehaviour

        private void OnValidate()
        {
            bool IsPrefabValid()
            {
                return _audioItemPrefab.TryGetComponent(out AudioSource audioSource) &&
                    _audioItemPrefab.TryGetComponent(out AudioPoolItem audioPoolItem);
            }

            _transform ??= GetComponent<Transform>();

            if (IsPrefabValid() == false)
            {
                Debug.LogError("AudioPool prefab must contain AudioSource and AudioPoolItem", this);
                _audioItemPrefab = null;
            }
        }

        private void Awake()
        {
            FillPool();
        }

        #endregion

        private void FillPool()
        {
            for (int i = 0; i < _soundsCount; i++)
            {
                CreatePoolItem();
            }
        }

        private void CreatePoolItem()
        {
            GameObject instantiatedPoolItem = _diContainer.InstantiatePrefab(_audioItemPrefab);
            instantiatedPoolItem.transform.SetParent(_transform);

            AudioPoolItem _audioPoolItem = instantiatedPoolItem.GetComponent<AudioPoolItem>();

            _audioPoolItem.audioSource = instantiatedPoolItem.GetComponent<AudioSource>();
            _audioPoolItem.positionLinker = instantiatedPoolItem.GetComponent<TransformPositionLinker>();

            _audioPoolItem.audioSource.rolloffMode = _rolloffMode;
            _audioPoolItem.audioSource.minDistance = _minSoundDistance;
            _audioPoolItem.audioSource.maxDistance = _maxSoundDistance;
            _audioPoolItem.ID = -1;

            instantiatedPoolItem.SetActive(false);

            _pool.Add(_audioPoolItem);
        }

        public int PlaySound(AudioMixerGroup output, AudioClip clip, Vector3 position, float volume,
            float spatialBlend, bool loop = false, PositionLinkerData linkData = null, int priority = 128)
        {
            if (CanPlay(clip, volume) == false) return 0;

            AudioPoolItem appropriatePoolItem = GetAppropriatePoolItem();

            return ConfigurePoolObject(appropriatePoolItem, output, clip, position, volume, spatialBlend, loop, linkData, priority);
        }

        private bool CanPlay(AudioClip clip, float volume)
        {
            if (Mathf.Approximately(volume, 0) || clip == null) return false;

            return true;
        }

        private AudioPoolItem GetAppropriatePoolItem()
        {
            AudioPoolItem appropriatePoolItem;

            if (TryGetFreePoolItem(out appropriatePoolItem))
            {
                return appropriatePoolItem;
            }

            return GetLeastImportantPoolItem();
        }

        private bool TryGetFreePoolItem(out AudioPoolItem audioPoolItem)
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                AudioPoolItem poolItem = _pool[i];

                if (poolItem.audioSource.isPlaying == false)
                {
                    audioPoolItem = poolItem;
                    return true;
                }
            }

            audioPoolItem = null;
            return false;
        }

        private AudioPoolItem GetLeastImportantPoolItem()
        {
            AudioPoolItem leastImportantAudioPoolItem = _pool[0];

            for (int i = 1; i < _pool.Count; i++)
            {
                AudioPoolItem poolItem = _pool[i];

                if (poolItem.priority > leastImportantAudioPoolItem.priority)
                {
                    leastImportantAudioPoolItem = poolItem;
                }
            }

            return leastImportantAudioPoolItem;
        }

        private int ConfigurePoolObject(AudioPoolItem poolItem, AudioMixerGroup output, AudioClip clip, Vector3 position, float volume,
            float spatialBlend, bool loop, PositionLinkerData linkData, int priority)
        {
            _idGiver++;

            poolItem.audioSource.outputAudioMixerGroup = output;
            poolItem.audioSource.clip = clip;
            poolItem.transform.position = position;
            poolItem.audioSource.volume = volume;
            poolItem.audioSource.spatialBlend = spatialBlend;
            poolItem.priority = priority;
            poolItem.audioSource.priority = priority;
            poolItem.ID = _idGiver;
            poolItem.audioSource.loop = loop;
            poolItem.gameObject.SetActive(true);
            poolItem.audioSource.Play();

            if (linkData != null)
            {
                poolItem.positionLinker.data.linkTo = linkData.linkTo;
                poolItem.positionLinker.data.offset = linkData.offset;
                poolItem.positionLinker.data.linkAxis = linkData.linkAxis;
                poolItem.positionLinker.enabled = true;
            }

            TryStopSoundDelayed(poolItem);

            _activePool[_idGiver] = poolItem;
            return _idGiver;
        }

        private void TryStopSoundDelayed(AudioPoolItem poolItem)
        {
            if (CanStopSoundDelayed(poolItem) == false) return;

            poolItem.waitTween = poolItem.DOWait(poolItem.audioSource.clip.length).OnComplete(() =>
            {
                StopSound(poolItem.ID);
            }).Play();
        }

        private bool CanStopSoundDelayed(AudioPoolItem poolItem)
        {
            return poolItem.audioSource.loop == false;
        }

        public void StopSound(int id)
        {
            if (_activePool.TryGetValue(id, out AudioPoolItem activeSound))
            {
                StopSound(activeSound);
            }
        }

        private void StopSound(AudioPoolItem activePoolItem)
        {
            activePoolItem.audioSource.Stop();
            activePoolItem.audioSource.clip = null;
            activePoolItem.positionLinker.data.linkTo = null;
            activePoolItem.positionLinker.enabled = false;
            activePoolItem.gameObject.SetActive(false);
            activePoolItem.waitTween.Kill();
            activePoolItem.ID = -1;

            _activePool.Remove(activePoolItem.ID);
        }

        public AudioPoolItem GetAudioPoolItem(int id)
        {
            _activePool.TryGetValue(id, out AudioPoolItem poolItem);

            return poolItem;
        }
    }
}
