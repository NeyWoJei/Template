using System;
using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public interface IUIService
    {
        void ShowMainMenu();
        UniTask HideMainMenu();
        void ShowLoadingScreen();
        void HideLoadingScreen();
        void ShowPauseMenu();
        UniTask HidePauseMenu();
        void ShowSettingsMenu();
        UniTask HideSettingsMenu();
        void ShowDialog(string message, Action onYes, Action onNo);
        UniTask HideDialog();
        void SetStartButtonAction(Action onClick);
    }
}