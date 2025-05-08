namespace Game.Core {
public interface IGameStateService {
    void SetPaused(bool isPaused);
    bool IsPaused {get; }
    }
}