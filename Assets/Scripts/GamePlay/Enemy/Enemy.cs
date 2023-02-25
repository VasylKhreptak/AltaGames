using System;
using GamePlay.Entity.Interfaces;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class Enemy : MonoBehaviour, IInfectable, IKillable
    {
        [Header("References")]
        [SerializeField] private GameObject _gameObject;

        #region MonoBehaviour

        private void OnValidate()
        {
            _gameObject ??= GetComponent<GameObject>();
        }

        #endregion

        public event Action onInfected;
        public event Action onKilled;

        public void Infect()
        {
            onInfected?.Invoke();
        }

        public void Kill()
        {
            _gameObject.SetActive(false);

            onKilled?.Invoke();
        }
    }
}
