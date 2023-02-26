using CBA.Actions.Core;
using ObjectPooler;
using UnityEngine;
using Zenject;

namespace Actions.ObjectPoolerManagement
{
    public class DisableObjectPool : Action
    {
        [Header("Preferences")]
        [SerializeField] private Pools _pool;

        private ObjectPooler.ObjectPooler _objectPooler;

        [Inject]
        private void Construct(ObjectPooler.ObjectPooler objectPooler)
        {
            _objectPooler = objectPooler;
        }

        public override void Do()
        {
            _objectPooler.DisablePool(_pool);
        }
    }
}
