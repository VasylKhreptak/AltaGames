using UnityEngine;

namespace Zenject.Installers
{
    public class ObjectPoolerInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private ObjectPooler.ObjectPooler _objectPooler;

        public override void InstallBindings()
        {
            Container.Bind<ObjectPooler.ObjectPooler>().FromInstance(_objectPooler).AsSingle();
        }
    }
}
