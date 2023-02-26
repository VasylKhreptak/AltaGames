using CBA.Events.Core;
using GamePlay.ShootLogic;
using UnityEngine;

namespace Events.ShootLogic
{
    public class OnTransferredAllMass : MonoEvent
    {
        [Header("References")]
        [SerializeField] private BallMassTransferrer _massTransferrer;

        #region MonoBehaviour

        private void OnValidate()
        {
            _massTransferrer ??= FindObjectOfType<BallMassTransferrer>();
        }

        private void Awake()
        {
            _massTransferrer.onTransferedAllMass += Invoke;
        }

        private void OnDestroy()
        {
            _massTransferrer.onTransferedAllMass -= Invoke;
        }

        #endregion
    }
}
