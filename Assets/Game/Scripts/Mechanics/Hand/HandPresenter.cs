using System;
using Zenject;
using DG.Tweening;
using Game.Scripts.ZenjectSystem;
using UnityEngine;

namespace Game.Scripts.Mechanics.Hand
{
    public class HandPresenter : IDisposable
    {
        private readonly HandView _handView;
        private SignalBus _signalBus;
        
        public HandPresenter(HandView handView)
        {
            _handView = handView;
        }

        [Inject]
        private void Constructor(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<HandMoveSignal>(MoveHandTo);
        }

        private void MoveHandTo(HandMoveSignal signal)
        {
            RectTransform transform = _handView.RectTransform;
            transform.DOMove(signal.MoveData.Position, 1f)
                .SetLink(_handView.gameObject);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<HandMoveSignal>(MoveHandTo);
        }
    }
}
