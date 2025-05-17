using Cysharp.Threading.Tasks;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.States
{
    public class GameplayState : IGameState
    {
        private const string GAME_SCENE_NAME = "GameScene";
        [Inject] private IUIService _uiService;

        public GameplayState()
        {
            Debug.Log("GameplayState запущен.");
        }

        public async UniTask Enter()
        {
            Debug.Log("Выбран GameplayState");

            await SceneManager.LoadSceneAsync(GAME_SCENE_NAME, LoadSceneMode.Single);

            await _uiService.HideLoadingScreen();
            
            _uiService.ShowMainMenu();

        }

        public UniTask Exit()
        {
            Debug.Log("Выход из GameplayState");
            return UniTask.CompletedTask;
        }
    }
}
