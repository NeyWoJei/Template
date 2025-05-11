namespace Game.Systems.Audio
{
    public readonly struct PlaySfxEvent
    {
         public readonly string SoundId;
         public PlaySfxEvent(string soundId)
            => SoundId = soundId;
    }
}