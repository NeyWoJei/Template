using Cysharp.Threading.Tasks;
using UnityEditor.Overlays;

namespace Game.Core
{
    public interface ISaveLoadService
    {
        UniTask SaveAsync(SaveData data);
        UniTask<SaveData> LoadAsync();
    }
}
