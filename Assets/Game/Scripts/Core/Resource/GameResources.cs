using Game.Scripts.Configurations;

namespace Game.Scripts.Core.Resource
{
    public class GameResources
    {
        public CosmeticConfig CosmeticConfig { get; private set; }
        public MakeupConfig MakeupConfig { get; private set; }
        
        public void SetResources(CosmeticConfig cosmetic, MakeupConfig makeup)
        {
            MakeupConfig = makeup;
            CosmeticConfig = cosmetic;
        }
    }
}