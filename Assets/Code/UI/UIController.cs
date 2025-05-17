using Cysharp.Threading.Tasks;
using Game.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using Game.States;

namespace Game.UI
{
    public class UIController : IInitializable
    {
        private const string GAME_SCENE_NAME = "GameScene";
        [Inject] private UIManager _uiManager;
        [Inject] private GameStateMachine _gameStateMachine;

        public UIController()
        {
            Debug.Log("UIController запущен.");
        }
        public void SetUIManager(UIManager uiManager)
        {
            _uiManager = uiManager;
        }
        public void Initialize()
        {
            Debug.Log("UIController инициализирован.");
            _uiManager.SetStartButtonAction(OnStartGame);
            _uiManager.SetQuitButtonMainMenuAction(OnQuitMainMenu);
            _uiManager.SetResumeButtonAction(OnResumeGame);
            _uiManager.SetQuitButtonPauseAction(OnQuitPauseMenu);
            _uiManager.SetSettingsButtonAction(OnOpenSettings);
            _uiManager.SetCloseSettingsButtonAction(OnCloseSettings);
        }

        private async void OnStartGame()
        {
            Debug.Log("OnStartGame вызван.");
            _uiManager.ShowLoadingScreen();
            await UniTask.Delay(1000);
            
            await _uiManager.HideMainMenu();
            await _gameStateMachine.Enter<GameplayState>();
            await SceneManager.LoadSceneAsync(GAME_SCENE_NAME, LoadSceneMode.Single);
            
        }

        private void OnQuitMainMenu()
        {
            Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }

        private void OnResumeGame()
        {
            _uiManager.HidePauseMenu().Forget();
            Time.timeScale = 1;
        }

        private void OnQuitPauseMenu()
        {
            _uiManager.HidePauseMenu().Forget();
            _uiManager.ShowMainMenu();
            Time.timeScale = 1;
        }

        private void OnOpenSettings()
        {
            _uiManager.ShowSettingsMenu();
        }

        private void OnCloseSettings()
        {
            _uiManager.HideSettingsMenu().Forget();
        }
    }
}
