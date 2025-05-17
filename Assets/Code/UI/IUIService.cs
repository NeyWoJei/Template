using System;
using Cysharp.Threading.Tasks;

namespace Game.UI
{
    public interface IUIService
    {
        void ShowMainMenu();
        UniTask HideMainMenu();
        void ShowLoadingScreen();
        UniTask HideLoadingScreen();
        void ShowPauseMenu();
        UniTask HidePauseMenu();
        void ShowSettingsMenu();
        UniTask HideSettingsMenu();
        void SetStartButtonAction(Action onClick);
    }
}