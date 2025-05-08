namespace Game.Core {
public class GameStateService : IGameStateService
{
    private bool _isPaused;

    public void SetPaused(bool isPaused) {
        _isPaused = isPaused;
    }

    public bool IsPaused => _isPaused;
    }
}