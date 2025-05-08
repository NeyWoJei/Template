using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Game.Core {
public class SceneLoader : ISceneLoader
{
    public async UniTask LoadSceneAsync(string sceneName
    ,LoadSceneMode mode = LoadSceneMode.Single
    ,CancellationToken cancellationToken = default) {
        await SceneManager.LoadSceneAsync(sceneName, mode)
        .ToUniTask(cancellationToken: cancellationToken);
        }   
    }
}