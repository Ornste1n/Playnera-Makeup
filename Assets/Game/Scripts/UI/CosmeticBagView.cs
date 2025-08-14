using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Scripts.Core.Resource;
using Game.Scripts.Mechanics.Makeup;

namespace Game.Scripts.UI
{
    public class CosmeticBagView : MonoBehaviour
    {
        [Header("UI references")]
        [SerializeField] private Image _cosmeticBackground;
        [SerializeField] private Image _blushBrush;
        [SerializeField] private Image _eyeBrush;
        [SerializeField] private Image _cream;
        [SerializeField] private Image _loofah;

        [Header("Tools references")]
        [SerializeField] private List<MakeupTool> _blushes;
        [SerializeField] private List<MakeupTool> _lipsticks;
        [SerializeField] private List<MakeupTool> _shadows;
        
        public List<MakeupTool> Blushes => _blushes;
        public List<MakeupTool> Lipsticks => _lipsticks;
        public List<MakeupTool> Shadows => _shadows;
        public Image BlushBrush => _blushBrush;
        
        [Inject]
        private void Constructor(GameResources resources) => PresetImages(resources);

        private void PresetImages(GameResources resources)
        {
            _cream.sprite = resources.CosmeticConfig.Cream;
            _loofah.sprite = resources.CosmeticConfig.Loofah;
            _eyeBrush.sprite = resources.CosmeticConfig.EyeBrush;
            _blushBrush.sprite = resources.CosmeticConfig.BlushBrush;
            _cosmeticBackground.sprite = resources.CosmeticConfig.CosmeticBackground;
        }
    }
}
