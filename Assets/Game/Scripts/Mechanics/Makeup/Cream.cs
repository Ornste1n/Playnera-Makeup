using Zenject;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Scripts.ZenjectSystem;
using Game.Scripts.Mechanics.Girl;

namespace Game.Scripts.Mechanics.Makeup
{
    public class Cream : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform m_Transform;
        
        private SignalBus _signalBus;
        
        [Inject]
        private void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _signalBus.Fire(new TakeMakeupToolSignal(new MakeupSignal(MakeupActions.ClearAcne), m_Transform, AnimationOnFace));
        }

        private Sequence AnimationOnFace(RectTransform hand, Vector3 itemWorld, GirlView girlView) // анимация крема
        {
            Sequence sequence = DOTween.Sequence();
            RectTransform face = girlView.FacePoint;
            Vector3 targetRight = face.position + new Vector3(100, 0, 0);
            Vector3 targetLeft = face.position + new Vector3(-200, 0, 0);

            sequence.Append(hand.DOMove(targetRight, 0.5f)
                    .SetEase(Ease.InOutSine))
                .Append(hand.DOMove(face.position, 0.5f)
                    .SetEase(Ease.InOutSine))
                .Append(hand.DOMove(targetLeft, 0.5f)
                    .SetEase(Ease.InOutSine))
                .Append(hand.DOMove(face.position, 0.5f)
                    .SetEase(Ease.InOutSine));

            return sequence;
        }
        
        private void OnValidate()
        {
            if (m_Transform == null)
                m_Transform = GetComponent<RectTransform>();
        }
    }
}