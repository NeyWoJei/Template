using System;
using Cysharp.Threading.Tasks;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game.States
{
    public class BootstrapState : IGameState, IInitializable
    {
        [Inject] private readonly ISaveLoadService _saveLoadService;
        [Inject] private readonly IGameStateService _gameStateService;
        private IGameStateMachine _stateMachine;
        private bool _isInitialized = false;

        public BootstrapState()
        {
            Debug.Log("BootstrapState запущен.");
        }

        public void SetStateMachine(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            if(_isInitialized) {
                Debug.Log("BootstrapState уже инициализирован, пропускаем повторный вызов");
                return;
            }
            _isInitialized = true;
            InitializeAsync().Forget();
            Debug.Log("BootstrapState Initialize вызван");
        }

        public async UniTask InitializeAsync()
        {
            Debug.Log("Запущен BootstrapState");
            try
            {
                if (_stateMachine == null || _saveLoadService == null || _gameStateService == null)
                {
                    Debug.LogError("BootstrapState зависимости не установлены");
                    return;
                }

                await LoadResources();

                SaveData saveData = await _saveLoadService.LoadAsync();
                if (saveData == null)
                {
                    Debug.LogError("Ошибка при загрузке данных");
                    return;
                }
                _gameStateService.SetPaused(saveData.IsPaused);
                await _stateMachine.Enter<LoadLevelState>();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка в BootstrapState Enter: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        public async UniTask Enter()
        {
            Debug.Log("Запущен BootstrapState");
            await InitializeAsync();
        }

        public UniTask Exit()
        {
            Debug.Log("BootstrapState Exit");
            return UniTask.CompletedTask;
        }

        private async UniTask LoadResources()
        {
            await UniTask.Delay(1000);
            Debug.Log("Ресурсы загрузились");
        }
    }
}