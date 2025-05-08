using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Core {
public interface ISceneLoader
{
    UniTask LoadSceneAsync(string sceneName
        ,LoadSceneMode mode = LoadSceneMode.Single
        ,CancellationToken cancellationToken = default);
    }
}
