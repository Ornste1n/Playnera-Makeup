using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Scripts.ZenjectSystem;

namespace Game.Scripts.Mechanics.Makeup
{
    public class Loofah : MonoBehaviour, IPointerClickHandler
    {
        private SignalBus _signalBus;
        
        [Inject]
        private void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _signalBus.Fire(new ClearFaceSignal());
        }
    }
}
