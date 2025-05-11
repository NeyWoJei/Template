namespace Game.Systems.Audio
{
    public interface IAudioSystem
    {
        void PlaySfx(string soundId);
        void PlayMusic(string musicId);
        void StopMusic();
    }
}