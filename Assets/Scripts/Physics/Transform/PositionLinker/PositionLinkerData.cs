using UnityEngine;

namespace Physics.Transform.PositionLinker
{
    [System.Serializable]
    public class PositionLinkerData
    {
        public UnityEngine.Transform linkTo;
        public Vector3Int linkAxis = Vector3Int.one;
        public Vector3 offset;
    }
}
