using Zenject;
using UnityEngine;
using Game.Scripts.UI;
using System.Collections.Generic;
using Game.Scripts.Core.Resource;
using Game.Scripts.ZenjectSystem;

namespace Game.Scripts.Mechanics.Makeup
{
    /// <summary>
    /// Класс-распределить и регистратор косметики
    /// </summary>
    public class MakeupToolsDistribute
    {
        private SignalBus _signalBus;
        private CosmeticBagView _cosmeticBagView;
        
        [Inject]
        private void Constructor(SignalBus signalBus, GameResources gameResources, CosmeticBagView bagView)
        {
            _signalBus = signalBus;
            _cosmeticBagView = bagView;
            
            DistributeTo(MakeupActions.Blush, bagView.Blushes, gameResources.MakeupConfig.Blush);
            DistributeTo(MakeupActions.Lipstick, bagView.Lipsticks, gameResources.MakeupConfig.Lipstick);
            DistributeTo(MakeupActions.Shadows, bagView.Shadows, gameResources.MakeupConfig.Shadows);
        }
        
        private void DistributeTo(MakeupActions makeup, List<MakeupTool> tools, List<Configurations.Makeup> config)
        {
            for (int i = 0; i < tools.Count; i++)
            {
                if (i >= config.Count) return;
                
                Sprite sprite = config[i].Tool;
                tools[i].Initialize(i, makeup, sprite);
                tools[i].Subscribe(HandleMakeupClick);
            }
        }

        private void HandleMakeupClick(MakeupTool makeupTool, RectTransform toolTransform)
        {
            MakeupSignal signal = new (makeupTool.Makeup, makeupTool.MakeupIndex);
            MakeupAnimationDelegate makeup = GetMakeupAnimation(makeupTool.Makeup);
            (ToolAnimationDelegate, RectTransform) toolAnimation = GetToolAnimation(makeupTool.Makeup);
            _signalBus.Fire(new TakeMakeupToolSignal(signal, makeupTool.RectTransform, makeup, toolAnimation));
        }

        // Распределение анимации предметов под тип косметики
        private (ToolAnimationDelegate, RectTransform) GetToolAnimation(MakeupActions actions)
        {
            return actions switch
            {
                MakeupActions.Blush => (MakeupAnimation.BrushTool, _cosmeticBagView.BlushBrush.rectTransform),
                MakeupActions.Shadows => (MakeupAnimation.BrushTool, _cosmeticBagView.ShadowBrush.rectTransform),
                _ => (null, null)
            };
        }
        
        // Распределение анимации макияжа под тип косметики
        private MakeupAnimationDelegate GetMakeupAnimation(MakeupActions actions)
        {
            return actions switch
            {
                MakeupActions.Lipstick => MakeupAnimation.Lipstick,
                MakeupActions.Blush => MakeupAnimation.BrushBlush,
                MakeupActions.Shadows => MakeupAnimation.Eye,
                _ => null
            };
        }
    }
}
