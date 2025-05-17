using Cysharp.Threading.Tasks;
using Game.UI;
using UnityEngine;
using VContainer;

namespace Game.States
{
    public class GameplayState : IGameState
    {
        private UIManager _uiManager;

        [Inject]
        public void Construct(UIManager uiManager)
        {
            _uiManager = uiManager;
        }

        public GameplayState()
        {
            Debug.Log("GameplayState запущен.");
        }

        public async UniTask Enter()
        {
            Debug.Log("Выбран GameplayState");

            _uiManager.HideLoadingScreen();
            await _uiManager.HideMainMenu();
        }

        public UniTask Exit()
        {
            Debug.Log("Выход из GameplayState");
            return UniTask.CompletedTask;
        }
    }
}
