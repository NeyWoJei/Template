namespace Game.Core {
public class GameStateService : IGameStateService
{
    private bool _isPaused;

    public GameStateService() {
        UnityEngine.Debug.Log("GameStateService запущен.");
    }

    public void SetPaused(bool isPaused) {
        _isPaused = isPaused;
    }

    public bool IsPaused => _isPaused;
    }
}