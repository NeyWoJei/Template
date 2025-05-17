using Cysharp.Threading.Tasks;
using Game.Core;
using UnityEngine;
using VContainer;

namespace Game.Systems
{
    public class CheckPointSystem
    {
        [Inject] private ISaveLoadService _saveLoadService;
        public CheckPointSystem() {
        Debug.Log("CheckPointSystem is Starting");
        }

        public async UniTask ReachCheckpoint(string levelName) {
            SaveData saveData = new SaveData {
                CurrentLevel = levelName,
                IsPaused = false
            };
            await _saveLoadService.SaveAsync(saveData);
        }
    }
}