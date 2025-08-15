using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using Game.Scripts.ZenjectSystem;
using Game.Scripts.Mechanics.Girl;

namespace Game.Scripts.Mechanics.Hand
{
    [RequireComponent(typeof(HandView))]
    public class HandDragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private HandView _handView;
        [SerializeField] private RectTransform m_CanvasRect;

        private bool _isDragging;
        private Vector2 _dragOffset;
        private GirlView _girlView;
        private SignalBus _signalBus;
        
        private RectTransform RectView => _handView.RectTransform;

        [Inject]
        private void Constructor(SignalBus signalBus, GirlView girlView)
        {
            _girlView = girlView;
            _signalBus = signalBus;
            _signalBus.Subscribe<ActivateDrag>(ActivateDrag);
            
            enabled = false;
        }

        private void ActivateDrag(ActivateDrag drag)
        {
            enabled = true;
        }

        #region Drag Handle
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_CanvasRect, eventData.position,
                    eventData.pressEventCamera, out Vector2 localPoint))
            {
                _dragOffset = (Vector2)RectView.localPosition - localPoint;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(m_CanvasRect, eventData.position,
                    eventData.pressEventCamera, out Vector2 localPoint))
            {
                RectView.localPosition = localPoint + _dragOffset;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!TryGetTipWorldPos(out Vector3 tipWorldPos)) return;
            if (!IsOverFace(tipWorldPos, eventData.pressEventCamera)) return;

            enabled = false;
            _signalBus.Fire(new DroppedOnFaceSignal(tipWorldPos));
        }
        #endregion

        // Нахожу кончик инструмента, для точного определения попадания по лицу
        private bool TryGetTipWorldPos(out Vector3 tipWorldPos) 
        {
            tipWorldPos = default;

            if (_handView.ItemPosition.childCount == 0) return false;

            RectTransform childItemRect = _handView.ItemPosition.GetChild(0) as RectTransform;
            if (childItemRect == null) return false;

            Vector3 localTop = new Vector3(0, childItemRect.rect.height * (1f - childItemRect.pivot.y), 0);
            tipWorldPos = childItemRect.TransformPoint(localTop);
            return true;
        }

        private bool IsOverFace(Vector3 worldPos, Camera eventCamera)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                _girlView.FacePoint, worldPos, eventCamera
            );
        }
        
        private void OnValidate()
        {
            if (_handView == null)
                _handView = GetComponent<HandView>();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ActivateDrag>(ActivateDrag);
        }
    }
}