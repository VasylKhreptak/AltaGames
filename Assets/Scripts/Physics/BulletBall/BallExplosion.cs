using CBA.Events.Core;
using GamePlay.Entity.Interfaces;
using UnityEngine;

namespace Physics.BulletBall
{
    public class BallExplosion : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private UnityEngine.Transform _transform;

        [Header("Events")]
        [SerializeField] private MonoEvent _explodeEvent;

        [Header("Explosion Preferences")]
        [SerializeField] private float _minAllowedScaleX;
        [SerializeField] private float _minScaleX;
        [SerializeField] private float _maxScaleX;
        [SerializeField] private float _minExplosionRadius;
        [SerializeField] private float _maxExplosionRadius;
        [SerializeField] private AnimationCurve _explosionRadiusCurve;
        [SerializeField] private int _maxAllowedInteractions = 10;

        [Header("Layer Preferences")]
        [SerializeField] private LayerMask _layerMask;

        #region MonoBehaviour

        private void OnValidate()
        {
            _transform ??= GetComponent<UnityEngine.Transform>();
            _explodeEvent ??= GetComponent<MonoEvent>();
        }

        private void Awake()
        {
            _explodeEvent.onMonoCall += TryExplode;
        }

        private void OnDestroy()
        {
            _explodeEvent.onMonoCall -= TryExplode;
        }

        #endregion

        private void TryExplode()
        {
            if (_transform.localScale.x < _minAllowedScaleX) return;

            float radius = GetExplosionRadius();

            Collider[] colliders = new Collider[_maxAllowedInteractions];

            int size = UnityEngine.Physics.OverlapSphereNonAlloc(_transform.position, radius, colliders, _layerMask);

            if (size == 0) return;

            for (var i = 0; i < size; i++)
            {
                if (colliders[i].TryGetComponent(out IInfectable infectable))
                {
                    infectable.Infect();
                }
            }
        }

        private float GetExplosionRadius()
        {
            return Extensions.AnimationCurve.Evaluate(_explosionRadiusCurve, _minScaleX, _maxScaleX, _transform.localScale.x,
                _minExplosionRadius, _maxExplosionRadius);
        }

        private void OnDrawGizmosSelected()
        {
            if (_transform == null) return;

            DrawExplosionRadiuses();
            DrawCurrentExplosionRadius();

            void DrawExplosionRadiuses()
            {
                Vector3 center = _transform.position;
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(center, _minExplosionRadius);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(center, _maxExplosionRadius);
            }

            void DrawCurrentExplosionRadius()
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(_transform.position, GetExplosionRadius());
            }
        }
    }
}
