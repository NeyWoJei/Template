using Cysharp.Threading.Tasks;
using Game.UI;
using VContainer;

namespace Game.States
{
    public class MenuState : IGameState
    {
        [Inject] private IUIService _uiService;

        public async UniTask Enter() {
            _uiService.ShowMainMenu();
            await UniTask.Delay(1000);
        }

        public UniTask Exit() {
            return UniTask.CompletedTask;
        }
    }
}