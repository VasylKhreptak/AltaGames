using System;

namespace GamePlay.Entity.Interfaces
{
    public interface IInfectable
    {
        public event Action onInfected;

        public void Infect();
    }
}
