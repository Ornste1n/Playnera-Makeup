using Zenject;
using Utilities;
using Game.Scripts.Core.Resource;

namespace Game.Scripts.ZenjectSystem.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ZenjectUtility.BindAndQueueForInject(Container, new GameResources());
            ZenjectUtility.BindAndQueueForInject(Container, new GameResourceLoader());
        }
    }
}