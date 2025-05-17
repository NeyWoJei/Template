using Cysharp.Threading.Tasks;

namespace Game.Core
{
    public interface ISaveLoadService
    {
        UniTask SaveAsync(SaveData data);
        UniTask<SaveData> LoadAsync();
    }
}
