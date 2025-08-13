using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Mechanics.Makeup
{
    [RequireComponent(typeof(Image))]
    public class MakeupTool : MonoBehaviour
    {
        [SerializeField] private Image m_Icon;
        
        public int MakeupIndex { get; private set; }
        
        public void Initialize(int index, Sprite icon)
        {
            MakeupIndex = index;
            m_Icon.sprite = icon;
        }

        private void OnValidate()
        {
            if (m_Icon == null)
                m_Icon = GetComponent<Image>();
        }
    }
}
