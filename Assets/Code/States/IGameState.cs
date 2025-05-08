using Cysharp.Threading.Tasks;

namespace Game.States {
public interface IGameState {
    UniTask Enter();
    UniTask Exit();
    }   
}