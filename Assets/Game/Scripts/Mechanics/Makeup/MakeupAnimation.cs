using DG.Tweening;
using UnityEngine;
using Game.Scripts.Mechanics.Girl;

namespace Game.Scripts.Mechanics.Makeup
{
    public delegate Sequence MakeupAnimationDelegate(RectTransform hand, Vector3 itemWorld, GirlView girlView);
    public delegate Sequence ToolAnimationDelegate(RectTransform hand, RectTransform item, RectTransform tool, GirlView view);
    
    /// <summary>
    /// Класс содержит в себе методы-анимация для различных преборов
    /// </summary>
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
        
        public static Sequence BrushBlush(RectTransform hand, Vector3 itemWorld, GirlView girlView)
        {
            Sequence sequence = DOTween.Sequence();
            RectTransform face = girlView.FacePoint;

            Vector3 offset = itemWorld - hand.position;

            Vector3 localRightEdge = new Vector3(face.rect.width * 0.5f, 0, 0);
            Vector3 localLeftEdge = new Vector3(-face.rect.width * 0.5f, 0, 0);

            Vector3 worldRightEdge = face.TransformPoint(localRightEdge);
            Vector3 worldLeftEdge = face.TransformPoint(localLeftEdge);
            
            sequence.Append(hand.DOMove(worldRightEdge - offset, 0.5f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldLeftEdge - offset, 1f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldRightEdge - offset, 1f).SetEase(Ease.InOutSine));

            return sequence;
        }
        
        public static Sequence Eye(RectTransform hand, Vector3 itemWorld, GirlView girlView)
        {
            Sequence sequence = DOTween.Sequence();

            RectTransform firstEye = girlView.FirstEyeShadowImage.rectTransform;
            RectTransform secondEye = girlView.SecondEyeShadowImage.rectTransform;

            Vector3 offset = itemWorld - hand.position;

            Vector3 localRightEdge1 = new Vector3(firstEye.rect.width * 0.5f, 0, 0);
            Vector3 localLeftEdge1  = new Vector3(-firstEye.rect.width * 0.5f, 0, 0);

            Vector3 worldRightEdge1 = firstEye.TransformPoint(localRightEdge1);
            Vector3 worldLeftEdge1  = firstEye.TransformPoint(localLeftEdge1);
            
            sequence.Append(hand.DOMove(worldRightEdge1 - offset, 0.45f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldLeftEdge1 - offset, 0.45f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldRightEdge1 - offset, 0.45f).SetEase(Ease.InOutSine));

            Vector3 localRightEdge2 = new Vector3(secondEye.rect.width * 0.5f, 0, 0);
            Vector3 localLeftEdge2  = new Vector3(-secondEye.rect.width * 0.5f, 0, 0);

            Vector3 worldRightEdge2 = secondEye.TransformPoint(localRightEdge2);
            Vector3 worldLeftEdge2  = secondEye.TransformPoint(localLeftEdge2);

            sequence.Append(hand.DOMove(worldRightEdge2 - offset, 0.45f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldLeftEdge2 - offset, 0.45f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldRightEdge2 - offset, 0.45f).SetEase(Ease.InOutSine));

            return sequence;
        }

        public static Sequence BrushTool(RectTransform hand, RectTransform itemTransform,
            RectTransform toolTransform, GirlView girlView)
        {
            Sequence sequence = DOTween.Sequence();

            Vector3 localTop = new Vector3(0, toolTransform.rect.height * (1f - toolTransform.pivot.y), 0);

            Vector3 tipOffset = toolTransform.TransformPoint(localTop) - hand.position;

            Vector3 worldRightEdge = itemTransform.TransformPoint(new Vector3(itemTransform.rect.width * 0.5f, 0, 0));
            Vector3 worldLeftEdge  = itemTransform.TransformPoint(new Vector3(-itemTransform.rect.width * 0.5f, 0, 0));

            sequence
                .Append(hand.DOMove(worldRightEdge - tipOffset, 0.45f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldLeftEdge - tipOffset, 0.45f).SetEase(Ease.InOutSine))
                .Append(hand.DOMove(worldRightEdge - tipOffset, 0.45f).SetEase(Ease.InOutSine))
                .AppendCallback(() =>
                {
                    Vector3 chestPosition = Vector3.Lerp(toolTransform.position, girlView.FacePoint.position, 0.35f);
                    hand.DOMove(chestPosition, 1f).SetEase(Ease.InOutSine);
                });

            return sequence;
        }
    }
}