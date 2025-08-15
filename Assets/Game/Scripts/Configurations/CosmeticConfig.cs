using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Scripts.Configurations
{
    [CreateAssetMenu(menuName = "Configuration/Create Cosmetic Config")]
    public class CosmeticConfig : ScriptableObject
    {
        [SerializeField] private Sprite _cosmeticBackground;
        [SerializeField] private Sprite _blushBrush;
        [SerializeField] private Sprite _eyeBrush;
        [SerializeField] private Sprite _cream;
        [SerializeField] private Sprite _loofah;
        [Space]
        [SerializeField] private List<CosmeticTabSettings> _cosmeticTabs = new();

        #region Properties
        public Sprite CosmeticBackground => _cosmeticBackground;
        public Sprite BlushBrush => _blushBrush;
        public Sprite Cream => _cream;
        public Sprite Loofah => _loofah;
        public Sprite EyeBrush => _eyeBrush;
        public List<CosmeticTabSettings> CosmeticTabs => _cosmeticTabs;
        #endregion
    }

    [Serializable]
    public struct CosmeticTabSettings
    {
        public Sprite DisableMode;
        public Sprite EnableMode;
    }
}