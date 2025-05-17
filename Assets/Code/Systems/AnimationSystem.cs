using DG.Tweening;
using UnityEngine;

namespace Game.Systems
{
    public class AnimationSystem : MonoBehaviour
    {
        public AnimationSystem() {
        Debug.Log("AnimationSystem is Starting");
        }
        public void MoveObject(Transform target, Vector3 endPosition, float duration) {
            target.DOMove(endPosition, duration).SetEase(Ease.InOutSine);
        }
        public void FadeSprite(SpriteRenderer sprite, float targetAlpha, float duration) {
            sprite.DOFade(targetAlpha, duration).SetEase(Ease.Linear);
        }
    }
}
