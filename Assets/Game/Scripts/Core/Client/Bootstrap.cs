using Zenject;
using DG.Tweening;
using UnityEngine;
using Game.Scripts.UI;
using System.Threading;
using Game.Scripts.Core.Resource;
using UnityEngine.SceneManagement;

namespace Game.Scripts.Core.Client
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ProgressBar _progressBar;

        private CancellationTokenSource _cancellation;
        private GameResourceLoader _gameResourceLoader;
        
        [Inject]
        private void Constructor(GameResourceLoader gameLoader)
        {
            _gameResourceLoader = gameLoader;
            _gameResourceLoader.OnLoadedEvent += HandleLoadedResource;

            _cancellation = new CancellationTokenSource();
            _ = _gameResourceLoader.LoadAsync(_cancellation.Token);
        }

        private void Start() => _progressBar.UpdateProgressBar(50);
        
        private void HandleLoadedResource()
        {
            _progressBar.UpdateProgressBar(100).OnComplete(SwitchScene);
        }
        
        private void SwitchScene() => SceneManager.LoadSceneAsync("Game");

        private void OnDestroy()
        {
            _cancellation?.Cancel();
            _gameResourceLoader.OnLoadedEvent -= HandleLoadedResource;
        }
    }
}
