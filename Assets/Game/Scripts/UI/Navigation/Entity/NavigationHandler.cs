using UnityEngine;
using System.Collections.Generic;

namespace Game.Scripts.UI.Navigation.Entity
{
    public abstract class NavigationHandler<T> : MonoBehaviour where T : NavigationPanel<T>
    {
        [Space, SerializeField] private T _startPanel;

        protected T CurrentPanel { get; set; }
        protected T StartPanel => _startPanel;

        protected virtual void Awake()
        {
            Subscribe();
            CurrentPanel = StartPanel;
        }

        protected abstract void Subscribe();

        protected abstract void SelectPanel(T navigationPanel);

        protected abstract void Unsubscribe();

        protected virtual void OnDestroy()
        {
            Unsubscribe();
        }
    }

    public abstract class NavigationList<T> : NavigationHandler<T> where T : NavigationPanel<T>
    {
        [SerializeField] private List<T> _panels = new();

        protected List<T> Panels => _panels;

        protected override void Subscribe()
        {
            foreach (T panel in _panels)
                panel.OnPanelClicked += SelectPanel;
        }

        protected override void Unsubscribe()
        {
            foreach (T panel in _panels)
                panel.OnPanelClicked -= SelectPanel;
        }
    }
}
