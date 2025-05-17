using System;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Game.UI
{
    public class UIService : IUIService
    {
        [Inject] private UIManager _uiManager;

        public UIService()
        {
            UnityEngine.Debug.Log("UIService запущец.");
        }

        public void ShowMainMenu()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.ShowMainMenu!");
                return;
            }
            uiManager.ShowMainMenu();
        }

        public UniTask HideMainMenu()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.HideMainMenu!");
                return UniTask.FromException(new InvalidOperationException("UIManager не инициализирован"));
            }
            return uiManager.HideMainMenu();
        }

        public void ShowLoadingScreen()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.ShowLoadingScreen!");
                return;
            }
            uiManager.ShowLoadingScreen();
        }

        public void HideLoadingScreen()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.HideLoadingScreen!");
                return;
            }
            uiManager.HideLoadingScreen();
        }

        public void ShowPauseMenu()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.ShowPauseMenu!");
                return;
            }
            uiManager.ShowPauseMenu();
        }

        public UniTask HidePauseMenu()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.HidePauseMenu!");
                return UniTask.FromException(new InvalidOperationException("UIManager не инициализирован"));
            }
            return uiManager.HidePauseMenu();
        }

        public void ShowSettingsMenu()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.ShowSettingsMenu!");
                return;
            }
            uiManager.ShowSettingsMenu();
        }

        public UniTask HideSettingsMenu()
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.HideSettingsMenu!");
                return UniTask.FromException(new InvalidOperationException("UIManager не инициализирован"));
            }
            return uiManager.HideSettingsMenu();
        }

        public void SetStartButtonAction(Action onClick)
        {
            var uiManager = _uiManager;
            if (uiManager == null)
            {
                UnityEngine.Debug.LogError("UIManager пуст в UIService.SetStartButtonAction!");
                return;
            }
            uiManager.SetStartButtonAction(onClick);
        }

        public void SetResumeButtonAction(Action onClick) { _uiManager?.SetResumeButtonAction(onClick); }
        public void SetQuitButtonMainMenuAction(Action onClick) { _uiManager?.SetQuitButtonMainMenuAction(onClick); }
        public void SetQuitButtonPauseAction(Action onClick) { _uiManager?.SetQuitButtonPauseAction(onClick); }
        public void SetSettingsButtonAction(Action onClick) { _uiManager?.SetSettingsButtonAction(onClick); }
        public void SetCloseSettingsButtonAction(Action onClick) { _uiManager?.SetCloseSettingsButtonAction(onClick); }
    }
}