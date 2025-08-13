using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _field;
        [SerializeField] private float _animationProgress;

        private const int MaxValue = 100;

        public Tween UpdateProgressBar(int value)
        {
            return AnimateFillAmount(_field, value, MaxValue, _animationProgress);
        }

        private Tween AnimateFillAmount(Image fillImage, int value, int maxValue, float animationDuration, bool invertValue = false)
        {
            float targetFillAmount = Mathf.Clamp01((float)value / maxValue);

            if (invertValue) targetFillAmount = 1 - targetFillAmount;
            
            float currentFillAmount = fillImage.fillAmount;
            return DOTween.To(() => currentFillAmount, x => currentFillAmount = x, targetFillAmount, animationDuration)
                .SetEase(Ease.InOutQuad).OnUpdate(() => {
                    if (fillImage != null)
                        fillImage.fillAmount = currentFillAmount;
                })
                .SetUpdate(true).SetAutoKill(true);
        }
    }
}
