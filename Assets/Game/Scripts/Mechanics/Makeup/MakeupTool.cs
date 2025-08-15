using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Game.Scripts.Mechanics.Makeup
{
    /// <summary>
    /// Класс-View для теней, помод и т.д. с чем можно взаимодействовать
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class MakeupTool : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image m_Icon;

        private RectTransform _actionTransform;
        private Action<MakeupTool, RectTransform> _onClick;
        
        public int MakeupIndex { get; private set; }
        public MakeupActions Makeup { get; private set; }
        public RectTransform RectTransform => m_Icon.rectTransform;

        public void Initialize(int index, MakeupActions makeup, Sprite icon)
        {
            Makeup = makeup;
            MakeupIndex = index;
            m_Icon.sprite = icon;
        }

        public void Subscribe(Action<MakeupTool, RectTransform> onClick)
        {
            _onClick = onClick;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick?.Invoke(this, _actionTransform ?? m_Icon.rectTransform);
        }
        
        private void OnValidate()
        {
            if (m_Icon == null)
                m_Icon = GetComponent<Image>();
        }
    }
}
