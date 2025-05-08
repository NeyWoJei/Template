using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Game.Core;
using Game.UI;
using VContainer;

namespace Game.States
{
    public class PauseState : IGameState
    {
        [Inject] private IGameStateService _gameStateService;
        [Inject] private UIManager _uiManager;
        [Inject] private ISaveLoadService _saveLoadService;

        public async UniTask Enter() {
            _gameStateService.SetPaused(true);
            _uiManager.ShowMainMenu();
            
            SaveData saveData = new SaveData {
                CurrentLevel = "LevelScene",
                IsPaused = true
            };
            await _saveLoadService.SaveAsync(saveData);
            await UniTask.CompletedTask;
        }
        public async UniTask Exit() {
            await _uiManager.HidePauseMenu();
            _gameStateService.SetPaused(false);
            await UniTask.CompletedTask;
        }
    }
}