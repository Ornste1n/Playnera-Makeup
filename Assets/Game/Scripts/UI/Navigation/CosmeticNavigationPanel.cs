using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Game.Scripts.Configurations;
using Game.Scripts.UI.Navigation.Entity;
using Game.Scripts.ZenjectSystem;
using Zenject;

namespace Game.Scripts.UI.Navigation
{
    [RequireComponent(typeof(Image))]
    public class CosmeticNavigationPanel : NavigationPanel<CosmeticNavigationPanel>
    {
        [SerializeField] private Image m_Panel;
        [SerializeField] private RectTransform _layout;
        [Space]
        [SerializeField] private float moveUpAmount = 20f;
        [SerializeField] private float animDuration = 0.3f;
        
        private Vector2 _startPos;
        private Vector3 _startScale;
        private CosmeticTabSettings m_Settings;
        
        private void Awake()
        {
            if (_layout == null) return;
            
            _startScale = m_Panel.rectTransform.localScale;
            _startPos = m_Panel.rectTransform.anchoredPosition;
        }
        
        public void Initialize(CosmeticTabSettings settings)
        {
            m_Settings = settings;
            m_Panel.sprite = m_Settings.DisableMode;
        }
        
        public void SetActivePanel(bool active)
        {
            RectTransform imageRect = m_Panel.rectTransform;
            
            _layout.gameObject.SetActive(active);
            imageRect.DOKill();

            if (active)
            {
                imageRect.DOScale(_startScale * 1.05f, animDuration).SetEase(Ease.OutBack);
                imageRect.DOAnchorPos(_startPos + Vector2.up * moveUpAmount, animDuration).SetEase(Ease.OutSine);
            }
            else
            {
                imageRect.DOScale(_startScale, animDuration).SetEase(Ease.InOutSine);
                imageRect.DOAnchorPos(_startPos, animDuration).SetEase(Ease.InOutSine);
            }

            m_Panel.sprite = active ? m_Settings.EnableMode : m_Settings.DisableMode;
        }

        private void OnValidate()
        {
            if (m_Panel != null) return;
            m_Panel = GetComponent<Image>();
        }
    }
}
