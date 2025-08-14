using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Create Hand Anim Config")]
    public class HandAnimationConfig : ScriptableObject
    {
        [SerializeField] private float _moveDuration;
        [SerializeField] private float _rotationDuration;
        [SerializeField] private Vector3 _moveOffset;
        [SerializeField] private Ease _moveEase;

        public Ease MoveEase => _moveEase;
        public float MoveDuration => _moveDuration;
        public Vector3 MoveOffset => _moveOffset;
        public float RotationDuration => _rotationDuration;
    }
}