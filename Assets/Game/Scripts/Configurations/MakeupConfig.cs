using System;
using UnityEngine;
using System.Collections.Generic;

namespace Game.Scripts.Configurations
{
    [CreateAssetMenu(menuName = "Configuration/Create Makeup Config")]
    public class MakeupConfig : ScriptableObject
    {
        [SerializeField] private List<Makeup> _lipstick = new();
        [SerializeField] private List<Makeup> _blush = new();
        [SerializeField] private List<Makeup> _shadows = new();
        
        public List<Makeup> Lipstick => _lipstick;
        public List<Makeup> Blush => _blush;
        public List<Makeup> Shadows => _shadows;
    }
    
    [Serializable]
    public struct Makeup
    {
        public Sprite Tool;
        public Sprite Result;
    }
}