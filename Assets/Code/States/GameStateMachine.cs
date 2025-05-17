using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _activeState;

        public GameStateMachine()
        {
            Debug.Log("StateMachine запущен.");
            _states = new Dictionary<Type, IGameState>();
        }

        public void AddState(IGameState state)
        {
            var stateType = state.GetType();
            Debug.Log($"Регистрируем состояние: {stateType.Name}");
            if (!_states.ContainsKey(stateType))
            {
                _states.Add(stateType, state);
            }
            else
            {
                Debug.LogWarning($"состояние {stateType.Name} уже зарегестрировано в GameStateMachine.");
            }
        }

        public async UniTask Enter<TState>() where TState : class, IGameState
        {
            Debug.Log($"Вход в состояние: {typeof(TState).Name}");
            try
            {
                if (_activeState != null)
                {
                    Debug.Log($"Выход из состояния: {_activeState.GetType().Name}");
                    await _activeState.Exit();
                }

                if (!_states.TryGetValue(typeof(TState), out var newState))
                {
                    Debug.LogError($"State {typeof(TState).Name} не найдены в _states!");
                    return;
                }

                _activeState = newState;
                await _activeState.Enter();
                Debug.Log($"Состояние {typeof(TState).Name} выбрано");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка входа в состояние {typeof(TState).Name}: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
    }
}