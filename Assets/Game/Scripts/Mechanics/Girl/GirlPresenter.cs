using System;
using Zenject;
using UnityEngine.UI;
using System.Collections.Generic;
using Game.Scripts.Core.Resource;
using Game.Scripts.ZenjectSystem;
using Game.Scripts.Mechanics.Makeup;

namespace Game.Scripts.Mechanics.Girl
{
    /// <summary>
    /// Обрабатывает события нанесения макияжа, управляет View
    /// </summary>
    public class GirlPresenter : IDisposable
    {
        private readonly GirlView _girlView;
        private SignalBus _signalBus;
        private GameResources _gameResources;
        
        public GirlPresenter(GirlView girlView) => _girlView = girlView;
        
        [Inject]
        private void Constructor(SignalBus signalBus, GameResources gameResources)
        {
            _signalBus = signalBus;
            _gameResources = gameResources;
            _signalBus.Subscribe<MakeupSignal>(HandleMakeupAction);
        }

        private void HandleMakeupAction(MakeupSignal signal)
        {
            int index = signal.MakeupIndex;
            
            switch (signal.Action)
            {
                case MakeupActions.ClearAcne:
                    _girlView.AcneContainer.gameObject.SetActive(false);
                    break;
                case MakeupActions.ClearFace:
                    ClearAllMake();
                    break;
                case MakeupActions.Blush:
                    SetMakeup(_girlView.BlushImage, _gameResources.MakeupConfig.Blush, index);
                    break;
                case MakeupActions.Lipstick:
                    SetMakeup(_girlView.LipstickImage, _gameResources.MakeupConfig.Lipstick, index);
                    break;
                case MakeupActions.Shadows:
                    SetMakeup(_girlView.FirstEyeShadowImage, _gameResources.MakeupConfig.Shadows, index);
                    SetMakeup(_girlView.SecondEyeShadowImage, _gameResources.MakeupConfig.Shadows, index);
                    break;
            }
        }

        private void SetMakeup(Image makeImage, List<Configurations.Makeup> makeup, int index)
        {
            if(index >= makeup.Count) return;

            makeImage.sprite = makeup[index].Result;

            if (!makeImage.enabled)
                makeImage.enabled = true;
        }

        private void ClearAllMake()
        {
            ClearMake(_girlView.BlushImage);
            ClearMake(_girlView.LipstickImage);
            ClearMake(_girlView.FirstEyeShadowImage);
            ClearMake(_girlView.SecondEyeShadowImage);
        }

        private void ClearMake(Image make)
        {
            make.sprite = null;
            make.enabled = false;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<MakeupSignal>(HandleMakeupAction);
        }
    }
}