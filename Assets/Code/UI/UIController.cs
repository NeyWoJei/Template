using Cysharp.Threading.Tasks;
using Game.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.UI
{
    public class UIController : IInitializable
    {
        [Inject] private UIManager _uiManager;
        public void Initialize()
        {
            _uiManager.SetStartButtonAction(OnStartGame);
            _uiManager.SetQuitButtonMainMenuAction(OnQuitMainMenu);
            _uiManager.SetResumeButtonAction(OnResumeGame);
            _uiManager.SetQuitButtonPauseAction(OnQuitPauseMenu);
            _uiManager.SetSettingsButtonAction(OnOpenSettings);
            _uiManager.SetCloseSettingsButtonAction(OnCloseSettings);
        }

        private void OnStartGame()
        {
            _uiManager.HideMainMenu().Forget();
            _uiManager.ShowLoadingScreen();

            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single).completed += (asyncOp) =>
            {
                _uiManager.HideLoadingScreen();
            };
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
