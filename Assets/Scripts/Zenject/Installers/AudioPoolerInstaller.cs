using UnityEngine;

namespace Zenject.Installers
{
    public class AudioPoolerInstaller : MonoInstaller
    {
        [Header("References")]
        [SerializeField] private GameObject _audioPoolerPrefab;

        public override void InstallBindings()
        {
            GameObject instantiatedObject = Container.InstantiatePrefab(_audioPoolerPrefab);

            Container.Bind<AudioPooler.AudioPooler>().FromComponentOn(instantiatedObject).AsSingle();
        }
    }
}
