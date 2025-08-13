using Zenject;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Configurations;
using Game.Scripts.Core.AddressablesSystem;

namespace Game.Scripts.Core.Resource
{
    public class GameResourceLoader : ResourceLoader
    {
        private GameResources _gameResources;

        [Inject]
        private void Constructor(GameResources gameResources)
        {
            _gameResources = gameResources;
        }
        
        protected override async UniTask LoadResource(CancellationToken token)
        {
            CosmeticConfig cosmeticConfig = await CosmeticConfigLoader.Load(token);
            MakeupConfig makeupConfig = await MakeupConfigLoader.Load(token);
            
            _gameResources.SetResources(cosmeticConfig, makeupConfig);
        }
    }
}