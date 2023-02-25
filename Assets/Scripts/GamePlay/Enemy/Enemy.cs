using System;
using GamePlay.Entity.Interfaces;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class Enemy : MonoBehaviour, IInfectable, IKillable
    {
        public event Action onInfected;
        public event Action onKilled;

        public void Infect()
        {
            onInfected?.Invoke();
        }

        public void Kill()
        {
            onKilled?.Invoke();
        }
    }
}
