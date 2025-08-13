using UnityEngine;
using UnityEngine.UI;
using Game.Scripts.Configurations;
using Game.Scripts.UI.Navigation.Entity;

namespace Game.Scripts.UI.Navigation
{
    [RequireComponent(typeof(Image))]
    public class CosmeticNavigationPanel : NavigationPanel<CosmeticNavigationPanel>
    {
        [SerializeField] private Image m_Panel;
        [SerializeField] private RectTransform _layout;

        private CosmeticTabSettings m_Settings;

        public void Initialize(CosmeticTabSettings settings)
        {
            m_Settings = settings;
            m_Panel.sprite = m_Settings.DisableMode;
        }
        
        public void SetActivePanel(bool active)
        {
            if(_layout != null) _layout.gameObject.SetActive(active);

            m_Panel.sprite = active ? m_Settings.EnableMode : m_Settings.DisableMode;
        }

        private void OnValidate()
        {
            if (m_Panel != null) return;
            m_Panel = GetComponent<Image>();
        }
    }
}