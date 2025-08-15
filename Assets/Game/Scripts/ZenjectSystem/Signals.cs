using UnityEngine;
using Game.Scripts.Mechanics.Makeup;

namespace Game.Scripts.ZenjectSystem
{
    public struct MakeupSignal
    {
        public readonly MakeupActions Action;
        public readonly int MakeupIndex;

        public MakeupSignal(MakeupActions action, int makeupIndex = -1)
        {
            Action = action;
            MakeupIndex = makeupIndex;
        }
    }
    
    public struct TakeMakeupToolSignal
    {
        public readonly MakeupSignal MakeupSignal;
        public readonly RectTransform ItemTransform;
        public readonly MakeupAnimationDelegate MakeupAnimation;
        public readonly (ToolAnimationDelegate Animation, RectTransform Tool) Tools;

        public TakeMakeupToolSignal
        (
            MakeupSignal signal, 
            RectTransform itemTransform, 
            MakeupAnimationDelegate makeupAnimation = null,
            (ToolAnimationDelegate, RectTransform) tools = default
        )
        {
            Tools = tools;
            MakeupSignal = signal;
            ItemTransform = itemTransform;
            MakeupAnimation = makeupAnimation;
        }
    }

    public struct EndMakeup { }
    public struct ActivateDrag { }

    public struct DroppedOnFaceSignal
    {
        public readonly Vector3 ItemWorldPosition;

        public DroppedOnFaceSignal(Vector3 itemWorldPosition)
        {
            ItemWorldPosition = itemWorldPosition;
        }
    }
    
}