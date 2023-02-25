using System;
using GamePlay.Entity.Interfaces;
using UnityEngine;

namespace GamePlay.Entity
{
    public class KillableObject : MonoBehaviour, IKillable
    {
        public event Action onKilled;

        public void Kill()
        {
            gameObject.SetActive(false);
            
            onKilled?.Invoke();
        }
    }
}
