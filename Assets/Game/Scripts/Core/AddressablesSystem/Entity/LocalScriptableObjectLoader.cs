using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Scripts.Core.AddressablesSystem.Entity
{
    public abstract class LocalScriptableObjectLoader
    {
        protected static async UniTask<T> LoadAllScriptableObjects<T>(string label, CancellationToken token)
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(label);

            try
            {
                while (!handle.IsDone)
                {
                    token.ThrowIfCancellationRequested();
                    
                    await UniTask.Yield();
                }

                return handle.Result;
            }
            catch (OperationCanceledException)
            {
                Addressables.Release(handle);
                throw;
            }
            catch (Exception)
            {
                Addressables.Release(handle);
                throw;
            }
        }
    }
}