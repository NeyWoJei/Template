using Cysharp.Threading.Tasks;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game.States
{
    public class BootstrapState : IGameState
    {
        [Inject] private IGameStateMachine _stateMachine;
        [Inject] private ISaveLoadService _saveLoadService;
        [Inject] private IGameStateService _gameStateService;

        public async UniTask Enter()
        {
            Debug.Log("Запущен BootstrapState");
            await LoadResources();
            SaveData saveData = await _saveLoadService.LoadAsync();
            _gameStateService.SetPaused(saveData.IsPaused);
            await _stateMachine.Enter<LoadLevelState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;

        private async UniTask LoadResources()
        {
            await UniTask.Delay(1000);
            Debug.Log("Ресурсы загрузились");
        }
    }
}