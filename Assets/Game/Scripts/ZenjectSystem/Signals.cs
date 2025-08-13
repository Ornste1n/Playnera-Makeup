using Game.Scripts.Mechanics.Hand;

namespace Game.Scripts.ZenjectSystem
{
    public struct ClearFaceSignal { }
    public struct ClearAcneSignal { }

    public struct HandMoveSignal
    {
        public HandMoveData MoveData;
    }
}