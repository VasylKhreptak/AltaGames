using System;
using DG.Tweening;
using Physics.Transform.PositionLinker;
using UnityEngine;

namespace AudioPooler
{
    [Serializable]
    public class AudioPoolItem : MonoBehaviour
    {
        public AudioSource audioSource;
        public TransformPositionLinker positionLinker;
        public Tween waitTween;
        public int priority;
        public int ID;

        #region MonoBehaviour

        private void OnDestroy()
        {
            waitTween.Kill();
        }

        #endregion
    }
}
