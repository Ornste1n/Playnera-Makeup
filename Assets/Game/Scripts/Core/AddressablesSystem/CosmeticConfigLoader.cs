using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Configurations;
using Game.Scripts.Core.AddressablesSystem.Entity;

namespace Game.Scripts.Core.AddressablesSystem 
{
    public abstract class CosmeticConfigLoader : LocalScriptableObjectLoader
    {
        private const string Label = "CosmeticConfig";

        public static async UniTask<CosmeticConfig> Load(CancellationToken token)
        {
            return await LoadAllScriptableObjects<CosmeticConfig>(Label, token);
        }
    }
    
    public abstract class MakeupConfigLoader : LocalScriptableObjectLoader
    {
        private const string Label = "MakeupConfig";

        public static async UniTask<MakeupConfig> Load(CancellationToken token)
        {
            return await LoadAllScriptableObjects<MakeupConfig>(Label, token);
        }
    }
}