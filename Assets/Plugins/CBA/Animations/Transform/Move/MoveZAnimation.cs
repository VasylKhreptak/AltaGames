using CBA.Animations.Transform.Move.Core;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace CBA.Animations.Transform.Move
{
    public class MoveZAnimation : DirectionMoveAnimation
    {
        public override Tween CreateForwardAnimation()
        {
            return _transform.DOMoveZ(_to, _duration, _snapping);
        }

        public override Tween CreateBackwardAnimation()
        {
            return _transform.DOMoveZ(_from, _duration, _snapping);
        }

        protected override void MoveTo(float axisPosition)
        {
            Vector3 position = _transform.position;
            _transform.position = new Vector3(position.x, position.y, axisPosition);
        }

        #region Editor

#if UNITY_EDITOR

        [ShowNonSerializedField] private bool _isRecording;

        [Button("Assign Start Position")]
        private void AssignStartPositionVariable()
        {
            if (_isRecording) return;

            _from = _transform.position.z;
        }

        [Button("Assign Target Position")]
        private void AssignTargetPosition()
        {
            if (_isRecording) return;

            _to = _transform.position.z;
        }

        [Button("Move To Start")]
        private void MoveToStartPositionEditor()
        {
            if (_isRecording) return;

            MoveToStartState();
        }

        [Button("Move To End")]
        private void MoveToTargetPosition()
        {
            if (_isRecording) return;

            MoveToEndState();
        }

        [Button("Start Recording")]
        private void StartRecording()
        {
            _from = _transform.position.z;

            _isRecording = true;
        }

        [Button("Stop Recording")]
        private void StopRecording()
        {
            if (_isRecording == false) return;

            _to = _transform.position.z;
            MoveToStartState();

            _isRecording = false;
        }
        
        private void OnDrawGizmosSelected()
        {
            if (_transform == null) return;

            if (_isRecording)
            {
                DrawRecordingArrow();
            }
            else
            {
                DrawDefaultArrow();
            }
        }

        private void DrawDefaultArrow()
        {
            Vector3 position = _transform.position;
            Vector3 startPosition = new Vector3(position.x, position.y, _from);
            Vector3 targetPosition = new Vector3(position.x, position.y, _to);
            Vector3 direction = targetPosition - startPosition;

            Gizmos.color = Color.blue;
            Extensions.Gizmos.DrawArrow(startPosition, direction);
        }

        private void DrawRecordingArrow()
        {
            Vector3 position = _transform.position;
            Vector3 startPosition = new Vector3(position.x, position.y, _from);
            Vector3 targetPosition = new Vector3(startPosition.x, startPosition.y, position.z);
            Vector3 direction = targetPosition - startPosition;

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(startPosition, 0.1f);
            Extensions.Gizmos.DrawArrow(startPosition, direction);
        }

#endif

        #endregion

    }
}
