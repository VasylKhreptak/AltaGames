using DG.Tweening;
using UnityEngine;

namespace Extensions
{
    public static class DoTween
    {
        public static Tween DOWait(this MonoBehaviour owner, float duration)
        {
            return DOTween.To(() => 0, _ => {}, 1f, duration);
        }
    }
}
