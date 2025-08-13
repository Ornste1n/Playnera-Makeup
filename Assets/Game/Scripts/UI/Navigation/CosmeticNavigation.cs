using System;
using Zenject;
using System.Collections.Generic;
using Game.Scripts.Core.Resource;
using Game.Scripts.UI.Navigation.Entity;
using UnityEngine;

namespace Game.Scripts.UI.Navigation
{
    public class CosmeticNavigation : NavigationList<CosmeticNavigationPanel>
    {
        [Inject]
        private void Constructor(GameResources resources)
        {
            List<CosmeticNavigationPanel> panels = Panels;
            for (int i = 0; i < panels.Count; i++)
            {
                if(i >= resources.CosmeticConfig.CosmeticTabs.Count) return;
                
                panels[i].Initialize(resources.CosmeticConfig.CosmeticTabs[i]);
            }
        }

        private void Start()
        {
            CurrentPanel.SetActivePanel(true);
        }

        protected override void SelectPanel(CosmeticNavigationPanel navigationPanel)
        {
            CurrentPanel.SetActivePanel(false);
            CurrentPanel = navigationPanel;
            CurrentPanel.SetActivePanel(true);
        }
    }
}
