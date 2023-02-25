using GamePlay.Entity;
using UnityEngine;
using Action = CBA.Actions.Core.Action;

namespace Actions.Entity
{
    public class KillObject : Action
    {
        [Header("References")]
        [SerializeField] private KillableObject _killableObject;

        #region MonoBehaviour

        private void OnValidate()
        {
            _killableObject ??= GetComponent<KillableObject>();
        }

        #endregion

        public override void Do()
        {
            _killableObject.Kill();
        }
    }
}
