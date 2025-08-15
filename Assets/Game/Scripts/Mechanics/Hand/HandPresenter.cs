using System;
using Zenject;
using DG.Tweening;
using UnityEngine;
using Game.Scripts.ZenjectSystem;
using Game.Scripts.Configurations;
using Game.Scripts.Mechanics.Girl;
using Game.Scripts.Mechanics.Makeup;

namespace Game.Scripts.Mechanics.Hand
{
    /// <summary>
    /// Запускает анимации руки, отвечает за переход между состояниями
    /// </summary>
    public class HandPresenter : IDisposable
    {
        private readonly HandView _handView;
        private GirlView _girlView;
        private SignalBus _signalBus;
        private HandAnimationConfig _animationConfig;

        #region Signal Properties
        private bool _itemTaken;
        private Vector3 _startHandPosition;
        private Vector3 _cachedItemPosition;
        private Transform _cachedItemParent;
        private RectTransform _cachedTransform;
        private TakeMakeupToolSignal _cachedSignal;
        #endregion
        
        public HandPresenter(HandView handView) => _handView = handView;

        [Inject]
        private void Constructor(SignalBus signalBus, GirlView girlView, HandAnimationConfig config)
        {
            _girlView = girlView;
            _signalBus = signalBus;
            _animationConfig = config;
            _signalBus.Subscribe<TakeMakeupToolSignal>(MoveHandTo);
            _signalBus.Subscribe<DroppedOnFaceSignal>(HandleDroppedOnFace);
        }

        #region Move Animations
        private void MoveHandTo(TakeMakeupToolSignal signal) // анимация подбора предемета
        {
            if(_itemTaken) return;
            
            _itemTaken = true;
            RectTransform itemTransform = signal.Tools.Tool ?? signal.ItemTransform;
            
            _cachedSignal = signal;
            _cachedTransform = itemTransform;
            _cachedItemParent = itemTransform.parent;
            _cachedItemPosition = itemTransform.position;
            _startHandPosition = _handView.RectTransform.position;
            
            HandAnimationConfig config = _animationConfig;
            RectTransform transform = _handView.RectTransform;
            float fastAnimationTime = config.MoveDuration / 2f;
            Vector3 targetPosition = itemTransform.position + config.MoveOffset;
            Vector3 diffPosition = Vector3.Lerp(itemTransform.position, _girlView.FacePoint.position, 0.25f);
            
            Sequence sequence = DOTween.Sequence();
            
            sequence.SetLink(_handView.gameObject).OnComplete(SequenceComplete);

            sequence.Append(transform.DOMove(targetPosition, config.MoveDuration)
                .SetEase(config.MoveEase));
            
            sequence.Append(transform.DORotate(new Vector3(0, 0, -30f), 
                config.RotationDuration));

            sequence.Append(transform.DOMove(itemTransform.position, fastAnimationTime)
                .SetEase(config.MoveEase).OnComplete(SetItemToHand));
            
            sequence.Append(transform.DORotate(new Vector3(0, 0, 0f), 
                config.RotationDuration));
            
            if(signal.Tools == default)
                sequence.Append(transform.DOMove(diffPosition, fastAnimationTime)
                    .SetEase(config.MoveEase));
            
            sequence.Play();
        }
        
        private void ReturnItem() // анимация возврата предемета
        {
            HandAnimationConfig config = _animationConfig;
            RectTransform transform = _handView.RectTransform;
            float fastAnimationTime = config.MoveDuration / 2f;
            Vector3 targetPosition = _cachedItemPosition + config.MoveOffset;

            Sequence sequence = DOTween.Sequence();
            sequence.SetLink(_handView.gameObject).OnComplete(OnReturnItem);

            sequence.Append(transform.DOMove(targetPosition, fastAnimationTime)
                .SetEase(config.MoveEase));
            
            sequence.Append(transform.DORotate(new Vector3(0, 0, -30f), 
                config.RotationDuration));

            sequence.Append(transform.DOMove(_cachedItemPosition, fastAnimationTime)
                .SetEase(config.MoveEase).OnComplete(SetItemToPlace));
            
            sequence.Append(transform.DORotate(new Vector3(0, 0, 0f), 
                config.RotationDuration));
            
            sequence.Append(transform.DOMove(_startHandPosition, config.MoveDuration)
                .SetEase(config.MoveEase));
            
            sequence.Play();
        }
        #endregion

        private void HandleDroppedOnFace(DroppedOnFaceSignal signal) // обработка события, когда предмет у лица
        {
            if (_cachedSignal.MakeupAnimation != null) // если есть анимация нанесия макияжа
            {
                Sequence s = _cachedSignal.MakeupAnimation?.Invoke(_handView.RectTransform, signal.ItemWorldPosition, _girlView);
                s.Play().OnComplete(FireMakeupEvent); // запускаем
            }
            else
                ReturnItem();
        }
        
        private void SequenceComplete()
        {
            (ToolAnimationDelegate Anim, RectTransform Transform) tools = _cachedSignal.Tools;
            
            if (tools.Anim != null) // если есть анимация подбора предмета (кисточки или помады)
            {
                Sequence sequence = tools.Anim?.Invoke(_handView.RectTransform, _cachedSignal.ItemTransform, tools.Transform, _girlView);
                sequence.Play().OnComplete(FireActivateDragEvent); // запускам
            }
            else
                FireActivateDragEvent();
        }

        #region Events Handle
        private void FireActivateDragEvent() => _signalBus.Fire(new ActivateDrag());
        private void FireMakeupEvent()
        {
            _signalBus.Fire(_cachedSignal.MakeupSignal);
            ReturnItem();
        }
        #endregion

        private void OnReturnItem()
        {
            _itemTaken = false;
            _signalBus.Fire(new EndMakeup());
        }

        private void SetItemToHand() => _cachedTransform.SetParent(_handView.ItemPosition);
        private void SetItemToPlace() => _cachedTransform.SetParent(_cachedItemParent);
        

        public void Dispose()
        {
            _signalBus.Unsubscribe<TakeMakeupToolSignal>(MoveHandTo);
        }
    }
}
