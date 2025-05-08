using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Game.UI
{
    public class UIController : MonoBehaviour
    {
        [Inject] private UIManager _uiManager;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _uiManager.SetStartButtonAction(OnStartGame);
            _uiManager.SetQuitButtonMainMenuAction(OnQuitMainMenu);
            _uiManager.SetResumeButtonAction(OnResumeGame);
            _uiManager.SetQuitButtonPauseAction(OnQuitPauseMenu);
            _uiManager.SetSettingsButtonAction(OnOpenSettings);
            _uiManager.SetCloseSettingsButtonAction(OnCloseSettings);
            _uiManager.SetYesButtonAction(OnDialogYes);
            _uiManager.SetNoButtonAction(OnDialogNo);
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

        private void OnDialogYes()
        {
            _uiManager.HideDialog().Forget();
        }

        private void OnDialogNo()
        {
            _uiManager.HideDialog().Forget();
        }

        public void ShowDialog(string message, Action onYes, Action onNo)
        {
            _uiManager.ShowDialog(message, onYes, onNo);
        }
    }
}
