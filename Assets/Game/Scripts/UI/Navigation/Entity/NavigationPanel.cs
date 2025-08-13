using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts.UI.Navigation.Entity
{
    public abstract class NavigationPanel<T> : MonoBehaviour, IPointerClickHandler where T : NavigationPanel<T>
    {
        public event Action<T> OnPanelClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPanelClicked?.Invoke(this as T);
        }
    }
}
