using UnityEngine;

namespace Game.Scripts.Mechanics.Hand
{
    [RequireComponent(typeof(RectTransform))]
    public class HandView : MonoBehaviour
    {
        [SerializeField] private RectTransform m_RectTransform;
        [SerializeField] private RectTransform _itemPosition;

        public RectTransform RectTransform => m_RectTransform;
        public RectTransform ItemPosition => _itemPosition;
        
        private void OnValidate()
        {
            if (m_RectTransform == null)
                m_RectTransform = GetComponent<RectTransform>();
        }
    }
}
