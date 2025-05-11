using Cysharp.Threading.Tasks;
using Game.Systems;
using UnityEngine;
using VContainer;

namespace Game.Core
{
    public class SaveLoadSystem : ISaveLoadService
    {
        [Inject] private EventBus _eventBus;
        private const string SaveKey = "GameSave";

        public async UniTask SaveAsync(SaveData data) {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
            await UniTask.Delay(100);
            _eventBus.TriggerEventWithId("save_completed");
            Debug.Log("Игра сохранена");
        }
        public async UniTask<SaveData> LoadAsync() {
            if (PlayerPrefs.HasKey(SaveKey)) {
                string json = PlayerPrefs.GetString(SaveKey);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                await UniTask.Delay(100);
                _eventBus.TriggerEventWithId("load_completed");
                Debug.Log("Игра загружена");
                return data;
            }
            
            SaveData defaultData = new SaveData();
            await UniTask.Delay(100);
            Debug.Log("Сохранения не найдены");
            return defaultData;
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public string CurrentLevel;
        public bool IsPaused;

        public SaveData() {
            CurrentLevel = "LevelScene";
            IsPaused = false;
        }
    }
}