using System;
using Cysharp.Threading.Tasks;
using VContainer;

namespace Game.UI
{
    public class UIService : IUIService
    {
        [Inject] private UIManager _uiManager;
        
        public void ShowMainMenu() {
            _uiManager.ShowMainMenu();
        }
        public UniTask HideMainMenu() {
            return _uiManager.HideMainMenu();
        }
        public void ShowLoadingScreen() {
            _uiManager.ShowLoadingScreen();
        }
        public void HideLoadingScreen() {
            _uiManager.HideLoadingScreen();
        }
        public void ShowPauseMenu() {
            _uiManager.ShowPauseMenu();
        }
        public UniTask HidePauseMenu() {
            return _uiManager.HidePauseMenu();
        }
        public void ShowSettingsMenu() {
            _uiManager.ShowSettingsMenu();
        }
        public UniTask HideSettingsMenu() {
            return _uiManager.HideSettingsMenu();
        }
        public void SetStartButtonAction(Action onClick) {
            _uiManager.SetStartButtonAction(onClick);
        }
    }
}