using Cysharp.Threading.Tasks;

namespace Game.States 
{
    public interface IGameStateMachine {
        UniTask Enter<TState>() where TState : class, IGameState;
    }
}