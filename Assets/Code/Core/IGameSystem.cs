namespace Game.Core
{
    public interface ITickable
    {
        void Tick();

    }
    public interface IInitializable
    {
        void Initialize();
    }
    public interface IFixedTickable
    {
        void FixedTick();
    }
}