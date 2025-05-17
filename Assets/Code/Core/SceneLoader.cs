using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Threading;
using Game.Core;
using UnityEngine;
using System;

namespace Game.Systems
{
    public class SceneLoader : ISceneLoader
    {
        public SceneLoader() {
        Debug.Log("SceneLoader запущен");
        }
        public async UniTask LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, CancellationToken cancellationToken = default)
        {
            try
            {
                Debug.Log($"Загрузка сцены: {sceneName}, mode: {mode}");
                await SceneManager.LoadSceneAsync(sceneName, mode).ToUniTask(cancellationToken: cancellationToken);
                Debug.Log($"Сцена {sceneName} загружена");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка загрузки сцены {sceneName}: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}