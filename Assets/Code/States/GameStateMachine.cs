using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
namespace Game.States 
{
    public class GameStateMachine : IGameStateMachine 
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _activeState;

        public GameStateMachine(IEnumerable<IGameState> states) {
            _states = new Dictionary<Type, IGameState>();

            foreach (var state in states) 
                _states.Add(state.GetType(), state);
        }

        public async UniTask Enter<TState>() where TState : class, IGameState {
            if (_activeState != null)
                await _activeState.Exit();
            var newState = _states[typeof(TState)];
            _activeState = newState;

            await _activeState.Enter();
        }
    }
}