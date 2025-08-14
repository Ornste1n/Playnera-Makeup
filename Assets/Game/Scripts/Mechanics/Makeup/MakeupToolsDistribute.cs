using System;
using Zenject;
using UnityEngine;
using DG.Tweening;
using Game.Scripts.UI;
using System.Collections.Generic;
using Game.Scripts.Core.Resource;
using Game.Scripts.ZenjectSystem;
using Game.Scripts.Mechanics.Girl;

namespace Game.Scripts.Mechanics.Makeup
{
    public class MakeupToolsDistribute : IDisposable
    {
        private SignalBus _signalBus;
        private CosmeticBagView _cosmeticBagView;
        private MakeupSignal _cachedMakeupSignal;
        
        [Inject]
        private void Constructor(SignalBus signalBus, GameResources gameResources, CosmeticBagView bagView)
        {
            _signalBus = signalBus;
            _cosmeticBagView = bagView;
            
            DistributeTo(MakeupActions.Blush, bagView.Blushes, gameResources.MakeupConfig.Blush);
            DistributeTo(MakeupActions.Lipstick, bagView.Lipsticks, gameResources.MakeupConfig.Lipstick);
            DistributeTo(MakeupActions.Shadows, bagView.Shadows, gameResources.MakeupConfig.Shadows);

            _signalBus.Subscribe<TakeMakeupToolSignal>(HandleMakeupTool);
            _signalBus.Subscribe<EndMakeupAnimation>(InvokeMakeupSignal);
        }

        private void HandleMakeup(MakeupTool makeupTool, RectTransform toolTransform)
        {
            MakeupSignal signal = new (makeupTool.Makeup, makeupTool.MakeupIndex);
            MakeupAnimationDelegate makeup = GetMakeupAnimation(makeupTool.Makeup);
            (ToolAnimationDelegate, RectTransform) toolAnimation = GetToolAnimation(makeupTool.Makeup);
            _signalBus.Fire(new TakeMakeupToolSignal(signal, makeupTool.RectTransform, makeup, toolAnimation));
        }
        
        private void DistributeTo(MakeupActions makeup, List<MakeupTool> tools, List<Configurations.Makeup> config)
        {
            for (int i = 0; i < tools.Count; i++)
            {
                if (i >= config.Count) return;
                
                Sprite sprite = config[i].Tool;
                tools[i].Initialize(i, makeup, sprite);
                tools[i].Subscribe(HandleMakeup);
            }
        }

        private void HandleMakeupTool(TakeMakeupToolSignal toolSignal) => _cachedMakeupSignal = toolSignal.MakeupSignal;

        private (ToolAnimationDelegate, RectTransform) GetToolAnimation(MakeupActions actions)
        {
            return actions switch
            {
                MakeupActions.Blush => (MakeupAnimation.BlushBrushTool, _cosmeticBagView.BlushBrush.rectTransform),
                _ => (null, null)
            };
        }
        
        private MakeupAnimationDelegate GetMakeupAnimation(MakeupActions actions)
        {
            return actions switch
            {
                MakeupActions.Lipstick => MakeupAnimation.Lipstick,
                MakeupActions.Blush => MakeupAnimation.Lipstick,
                MakeupActions.Shadows => MakeupAnimation.Lipstick,
                _ => null
            };
        }
        
        private void InvokeMakeupSignal()
        {
            _signalBus.Fire(_cachedMakeupSignal);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EndMakeupAnimation>(InvokeMakeupSignal);
            _signalBus.Unsubscribe<TakeMakeupToolSignal>(HandleMakeupTool);
        }
    }
}
