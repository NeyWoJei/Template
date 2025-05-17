using Cysharp.Threading.Tasks;
using Game.Core;
using VContainer;
using UnityEngine;
using Game.UI;

namespace Game.States
{
    public class LoadLevelState : IGameState
    {
        [Inject] private ISceneLoader _sceneLoader;
        [Inject] private IGameStateMachine _stateMachine;
        [Inject] private ISaveLoadService _saveLoadService;

        public LoadLevelState() {
            Debug.Log("LoadLevelState запущен.");
        }

        public async UniTask Enter() {
            SaveData saveData = await _saveLoadService.LoadAsync();
            await _sceneLoader.LoadSceneAsync(saveData.CurrentLevel);
            
            await UniTask.NextFrame(); 

            await _stateMachine.Enter<MenuState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}
