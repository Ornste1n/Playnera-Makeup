using Zenject;
using System.Collections.Generic;
using Game.Scripts.Core.Resource;
using Game.Scripts.ZenjectSystem;
using Game.Scripts.UI.Navigation.Entity;

namespace Game.Scripts.UI.Navigation
{
    public class CosmeticNavigation : NavigationList<CosmeticNavigationPanel>
    {
        private SignalBus _signalBus;
        private bool _panelsEnabled = true;
        
        [Inject]
        private void Constructor(SignalBus signalBus, GameResources resources)
        {
            List<CosmeticNavigationPanel> panels = Panels;
            for (int i = 0; i < panels.Count; i++)
            {
                if(i >= resources.CosmeticConfig.CosmeticTabs.Count) return;
                
                panels[i].Initialize(resources.CosmeticConfig.CosmeticTabs[i]);
            }
            
            _signalBus = signalBus;
            _signalBus.Subscribe<EndMakeup>(ActivatePanel);
            _signalBus.Subscribe<TakeMakeupToolSignal>(DeactivatePanel);
        }
        
        private void Start()
        {
            CurrentPanel.SetActivePanel(true);
        }

        protected override void SelectPanel(CosmeticNavigationPanel navigationPanel)
        {
            if(!_panelsEnabled) return;
            
            CurrentPanel.SetActivePanel(false);
            CurrentPanel = navigationPanel;
            CurrentPanel.SetActivePanel(true);
        }
        
        private void ActivatePanel() => _panelsEnabled = true;
        private void DeactivatePanel() => _panelsEnabled = false;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _signalBus.Unsubscribe<EndMakeup>(ActivatePanel);
            _signalBus.Unsubscribe<TakeMakeupToolSignal>(DeactivatePanel);
        }
    }
}
