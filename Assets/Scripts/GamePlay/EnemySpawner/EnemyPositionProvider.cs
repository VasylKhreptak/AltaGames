using System.Collections.Generic;
using Extensions;
using NaughtyAttributes;
using UnityEngine;

namespace GamePlay.EnemySpawner
{
    public class EnemyPositionProvider : MonoBehaviour
    {
        [Header("Preferences")]
        [SerializeField] private List<Vector3> _positions = new List<Vector3>();

        public int PositionsCount => _positions.Count;

        public IEnumerable<Vector3> Generator()
        {
            _positions.Shuffle();

            for (var i = 0; i < _positions.Count; i++)
            {
                yield return _positions[i];
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_positions == null || _positions.Count == 0) return;

            Gizmos.color = Color.red;

            foreach (var position in _positions)
            {
                DrawPosition(position);
            }

            void DrawPosition(Vector3 position)
            {
                Gizmos.DrawSphere(position, 0.3f);
            }
        }

        #region Editor

#if UNITY_EDITOR

        [Button("Add Current Position")]
        private void AddCurrentPosition()
        {
            Vector3 position = transform.position;

            if (_positions.Contains(position) == false)
            {
                _positions.Add(position);
            }
        }

        [Button("Clear")]
        private void Clear()
        {
            _positions.Clear();
        }

        [Button("Remove Last")]
        private void Undo()
        {
            _positions.Remove(_positions.Last());
        }

        [Button("Fix Y")]
        private void FixY()
        {
            for (var i = 0; i < _positions.Count; i++)
            {
                var position = _positions[i];
                Ray ray = new Ray(new Vector3(position.x, position.y + 20, position.z), Vector3.down);

                if (UnityEngine.Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    _positions[i] = new Vector3(position.x, hitInfo.point.y, position.z);
                }
            }
        }

#endif

        #endregion

    }
}
