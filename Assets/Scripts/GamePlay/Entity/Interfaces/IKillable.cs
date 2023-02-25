using System;

namespace GamePlay.Entity.Interfaces
{
    public interface IKillable
    {
        public event Action onKilled;

        public void Kill();
    }
}
