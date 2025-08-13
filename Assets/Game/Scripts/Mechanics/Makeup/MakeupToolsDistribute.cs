using Zenject;
using UnityEngine;
using Game.Scripts.UI;
using System.Collections.Generic;
using Game.Scripts.Core.Resource;

namespace Game.Scripts.Mechanics.Makeup
{
    public class MakeupToolsDistribute
    {
        private GameResources _gameResources;
        private CosmeticBagView _cosmeticBagView;
        
        [Inject]
        private void Constructor(GameResources gameResources, CosmeticBagView bagView)
        {
            DistributeTo(bagView.Blushes, gameResources.MakeupConfig.Blush);
            DistributeTo(bagView.Lipsticks, gameResources.MakeupConfig.Lipstick);
            DistributeTo(bagView.Shadows, gameResources.MakeupConfig.Shadows);
        }

        private void DistributeTo(List<MakeupTool> tools, List<Configurations.Makeup> config)
        {
            for (int i = 0; i < tools.Count; i++)
            {
                if (i >= config.Count) return;
                
                Sprite sprite = config[i].Tool;
                tools[i].Initialize(i, sprite);
            }
        }
    }
}
