using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.States
{
    public class GameplayState : IGameState
    {
        public async UniTask Enter() {
            Debug.Log("Выбран GameplayState");
            await UniTask.CompletedTask;
        }
        public UniTask Exit() {
            Debug.Log("Выход из GameplayState");
            return UniTask.CompletedTask;
        }
    }
}