using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Physics.Transform.PositionLinker
{
    [System.Serializable]
    public class TransformPositionLinker : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UnityEngine.Transform _transform;

        [Header("Link Preferences")]
        public PositionLinkerData data;

        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<UnityEngine.Transform>();

            if (data == null) return;

            data.linkAxis = CBA.Extensions.Vector3Int.ClampComponents01(data.linkAxis);
        }

        private void Update()
        {
            if (data.linkTo == null || data.linkAxis == Vector3Int.zero) return;

            Vector3 position = CBA.Extensions.Vector3.ReplaceWithByAxes(_transform.position, data.linkTo.position + data.offset, data.linkAxis);

            _transform.position = position;
        }

        #endregion
    }
}
