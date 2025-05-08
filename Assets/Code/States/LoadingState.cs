using Cysharp.Threading.Tasks;
using Game.UI;
using VContainer;

namespace Game.States
{
    public class LoadingState : IGameState
    {
        [Inject] private IUIService _uiService;

        public async UniTask Enter() {
            _uiService.ShowMainMenu();
            await LoadGameData();
            _uiService.HideLoadingScreen();
        }
        public UniTask Exit() {
            return UniTask.CompletedTask;
        }
        public async UniTask LoadGameData() {
            await UniTask.Delay(2000);
        }
    }
}