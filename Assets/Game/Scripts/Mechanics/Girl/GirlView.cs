using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Mechanics.Girl
{
    [RequireComponent(typeof(Image))]
    public class GirlView : MonoBehaviour
    {
        [SerializeField] private Image m_Icon;

        [Header("Makeup image")]
        [SerializeField] private Image _blushImage;
        [SerializeField] private Image _lipstickImage;
        [SerializeField] private Image _firstEyeShadowImage;
        [SerializeField] private Image _secondEyeShadowImage;
        [SerializeField] private RectTransform _acneContainer;

        #region Properties
        public Image Girl => m_Icon;
        public Image BlushImage => _blushImage;
        public Image LipstickImage => _lipstickImage;
        public Image FirstEyeShadowImage => _firstEyeShadowImage;
        public Image SecondEyeShadowImage => _secondEyeShadowImage;
        public RectTransform AcneContainer => _acneContainer;
        #endregion
        
        private void OnValidate()
        {
            if (m_Icon == null)
                m_Icon = GetComponent<Image>();
        }
    }
}
