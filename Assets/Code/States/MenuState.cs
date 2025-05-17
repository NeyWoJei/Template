using System;
using Cysharp.Threading.Tasks;
using Game.UI;
using VContainer;

namespace Game.States
{
    public class MenuState : IGameState
    {
        [Inject] private IUIService _uiService;

        public MenuState()
        {
            UnityEngine.Debug.Log("MenuState запущен.");
        }

        public async UniTask Enter()
        {
            UnityEngine.Debug.Log("Выбран MenuState");
            try
            {
                var uiService = _uiService; 
                await uiService.HideLoadingScreen();
                
                uiService.ShowMainMenu();
                await UniTask.Delay(1000);
                UnityEngine.Debug.Log("MenuState завершает Enter");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"Ошибка при доступе к IUIService: {ex.Message}");
                throw;
            }
        }

        public UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}