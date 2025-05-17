using System.Collections.Generic;
using Code.Systems.Events;
using Cysharp.Threading.Tasks;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game.Systems
{
    public class InputSystem : IInitializable
    {
        [Inject] private EventBus _eventBus;

        private Dictionary<KeyCode, string> _keyEventMap = new();
        private bool _listening;

        public InputSystem()
        {
            Debug.Log("InputSystem is Starting");
        }
        public void Initialize()
        {
            RegisterKey(KeyCode.Escape, InputEvents.ESC);
            StartListening();
        }

        public void RegisterKey(KeyCode key, string eventId)
        {
            if (!_keyEventMap.ContainsKey(key))
                _keyEventMap.Add(key, eventId);
        }

        public void UnregisterKey(KeyCode key)
        {
            if (_keyEventMap.ContainsKey(key))
                _keyEventMap.Remove(key);
        }

        public void StartListening()
        {
            if (_listening) return;
            _listening = true;
            ListenLoop().Forget();
        }

        public void StopListening()
        {
            _listening = false;
        }

        private async UniTaskVoid ListenLoop()
        {
            while (_listening)
            {
                foreach (var pair in _keyEventMap)
                {
                    if (Input.GetKeyDown(pair.Key))
                    {
                        _eventBus.TriggerEventWithId(pair.Value);
                    }
                }

                await UniTask.Yield();
            }
        }
        

        public async UniTask<bool> WaitForDoublePress(KeyCode keyCode, float timeLimit) {
            bool firstPress = await WaitForKeyPress(keyCode, timeLimit);
            if (!firstPress) {
                return false;
            }

            bool secondPress = await WaitForKeyPress(keyCode, timeLimit);
            if (secondPress) {
                _eventBus.TriggerEventWithId("double_press_" + keyCode);
                return true;
            }
            return false;
        }
        private async UniTask<bool> WaitForKeyPress(KeyCode keyCode, float timeLimit) {
            float elapsed = 0f;

            while (elapsed < timeLimit) {
                if (Input.GetKeyDown(keyCode)) {
                    return true;
                }
                await UniTask.Yield();
                elapsed += Time.deltaTime;
            }
            return false;
        }
        public async UniTask<bool> WaitForAnyInput(float timeLimit) {
            float elapsed = 0f;
            
            while (elapsed < timeLimit) {
                if(Input.anyKeyDown || Input.GetMouseButton(0)) {
                    return true;
                }
                await UniTask.Yield();
                elapsed += Time.deltaTime;
            }
            return false;
        }
    }
}
