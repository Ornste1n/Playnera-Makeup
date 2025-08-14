using DG.Tweening;
using UnityEngine;
using Game.Scripts.Mechanics.Girl;

namespace Game.Scripts.Mechanics.Makeup
{
    public delegate Sequence MakeupAnimationDelegate(RectTransform hand, Vector3 itemWorld, GirlView girlView);
    public delegate Sequence ToolAnimationDelegate(RectTransform hand, RectTransform item, RectTransform tool);
    
    public static class MakeupAnimation
    {
        public static Sequence Lipstick(RectTransform hand, Vector3 itemWorld, GirlView girlView)
        {
            Sequence sequence = DOTween.Sequence();
            RectTransform lips = girlView.LipstickImage.rectTransform;

            Vector3 offset = itemWorld - hand.position;

            Vector3 localRightEdge = new Vector3(lips.rect.width * 0.5f, 0, 0);
            Vector3 localLeftEdge = new Vector3(-lips.rect.width * 0.5f, 0, 0);

            Vector3 worldRightEdge = lips.TransformPoint(localRightEdge);
            Vector3 worldLeftEdge = lips.TransformPoint(localLeftEdge);
            
            sequence.Append(hand.DOMove(worldRightEdge - offset, 0.5f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldLeftEdge - offset, 1f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldRightEdge - offset, 1f).SetEase(Ease.InOutSine));

            return sequence;
        }
        
        public static Sequence BlushBrushTool(RectTransform hand, RectTransform itemTransform, RectTransform toolTransform)
        {
            Sequence sequence = DOTween.Sequence();

            Vector3 localTip = new Vector3( toolTransform.rect.width * (1f - toolTransform.pivot.x),  0,  0);
            Vector3 worldTip = toolTransform.TransformPoint(localTip);

            Vector3 offset = worldTip - hand.position;

            Vector3 localRightEdge = new Vector3(itemTransform.rect.width * 0.5f, 0, 0);
            Vector3 localLeftEdge = new Vector3(-itemTransform.rect.width * 0.5f, 0, 0);

            Vector3 worldRightEdge = itemTransform.TransformPoint(localRightEdge);
            Vector3 worldLeftEdge = itemTransform.TransformPoint(localLeftEdge);

            sequence.Append(hand.DOMove(itemTransform.position - offset, 0.5f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldRightEdge - offset, 0.5f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldLeftEdge - offset, 1f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldRightEdge - offset, 1f).SetEase(Ease.InOutSine));

            return sequence;
        }

    }
}