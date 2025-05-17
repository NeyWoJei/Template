using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Game.Systems
{
    public class InputSystem
    {
        [Inject] private EventBus _eventBus;

        public InputSystem() {
        Debug.Log("inputSystem is Starting");
        }
        public void CheckKeyPress(KeyCode keyCode, string eventId) {
            if (Input.GetKeyDown(keyCode)) {
                _eventBus.TriggerEventWithId(eventId);
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
