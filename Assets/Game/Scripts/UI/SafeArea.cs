using UnityEngine;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private RectTransform m_RectTransform;
        
        private void Start()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;

            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            m_RectTransform.anchorMin = anchorMin;
            m_RectTransform.anchorMax = anchorMax;
        }
        
        private void OnValidate()
        {
            if (m_RectTransform == null)
                m_RectTransform = GetComponent<RectTransform>();
        }
    }
}
