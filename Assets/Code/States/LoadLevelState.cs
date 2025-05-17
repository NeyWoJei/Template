using Cysharp.Threading.Tasks;
using VContainer;
using UnityEngine;
using Game.UI;

namespace Game.States
{
    public class LoadLevelState : IGameState
    {
        [Inject] private IGameStateMachine _stateMachine;
 

        public LoadLevelState() {
            Debug.Log("LoadLevelState запущен.");
        }

        public async UniTask Enter() {
            await _stateMachine.Enter<MenuState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}
