using Zenject;
using Utilities;
using UnityEngine;
using Game.Scripts.UI;
using Game.Scripts.Configurations;
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
        [SerializeField] private HandAnimationConfig _handAnimation;

        public override void InstallBindings()
        {
            RegisterSignals();
            Container.Bind<GirlView>().FromInstance(_girlView);
            Container.Bind<CosmeticBagView>().FromInstance(_cosmeticBagView);
            Container.Bind<HandAnimationConfig>().FromInstance(_handAnimation).AsCached();
            
            ZenjectUtility.BindAllAndQueueForInject(Container, new MakeupToolsDistribute());
            ZenjectUtility.BindAllAndQueueForInject(Container, new HandPresenter(_handView));
            ZenjectUtility.BindAllAndQueueForInject(Container, new GirlPresenter(_girlView));
        }

        private void RegisterSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<ActivateDrag>();
            Container.DeclareSignal<MakeupSignal>();
            Container.DeclareSignal<TakeMakeupToolSignal>();
            Container.DeclareSignal<EndMakeupAnimation>();
            Container.DeclareSignal<DroppedOnFaceSignal>();
        }
    }
}