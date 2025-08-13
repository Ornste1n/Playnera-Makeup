using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Scripts.ZenjectSystem;
using Game.Scripts.Mechanics.Hand;

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
            _signalBus.Fire(new HandMoveSignal()
            {
                MoveData = new HandMoveData { Position = m_Transform.position }
            });
        }

        private void OnValidate()
        {
            if (m_Transform == null)
                m_Transform = GetComponent<RectTransform>();
        }
    }
}