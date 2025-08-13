using Zenject;
using Utilities;
using UnityEngine;
using Game.Scripts.UI;
using Game.Scripts.Mechanics.Girl;
using Game.Scripts.Mechanics.Hand;
using Game.Scripts.Mechanics.Makeup;

namespace Game.Scripts.ZenjectSystem.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private HandView _handView;
        [SerializeField] private GirlView _girlView;
        [SerializeField] private CosmeticBagView _cosmeticBagView;

        public override void InstallBindings()
        {
            RegisterSignals();
            Container.Bind<CosmeticBagView>().FromInstance(_cosmeticBagView);
            
            ZenjectUtility.BindAndQueueForInject(Container, new MakeupToolsDistribute());
            ZenjectUtility.BindAndQueueForInject(Container, new HandPresenter(_handView));
            ZenjectUtility.BindAllAndQueueForInject(Container, new GirlPresenter(_girlView));
        }

        private void RegisterSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ClearFaceSignal>();
            Container.DeclareSignal<ClearAcneSignal>();
            Container.DeclareSignal<HandMoveSignal>();
        }
    }
}