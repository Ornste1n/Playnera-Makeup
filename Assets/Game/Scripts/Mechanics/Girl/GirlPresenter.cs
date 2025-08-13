using System;
using Zenject;
using Game.Scripts.ZenjectSystem;

namespace Game.Scripts.Mechanics.Girl
{
    public class GirlPresenter : IDisposable
    {
        private readonly GirlView _girlView;
        private SignalBus _signalBus;
        
        public GirlPresenter(GirlView girlView) => _girlView = girlView;
        
        [Inject]
        private void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<ClearAcneSignal>(ClearAcne);
            _signalBus.Subscribe<ClearFaceSignal>(ClearAllMake);
        }

        private void ClearAcne()
        {
            _girlView.AcneContainer.gameObject.SetActive(false);
        }
        
        private void ClearAllMake()
        {
            UnityEngine.Debug.Log("ClearAllMake");
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ClearAcneSignal>(ClearAcne);
            _signalBus.Unsubscribe<ClearFaceSignal>(ClearAllMake);
        }
    }
}