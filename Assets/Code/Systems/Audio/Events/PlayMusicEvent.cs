namespace Game.Systems.Audio
{
    public readonly struct PlayMusicEvent
    {
         public readonly string MusicId;
         public PlayMusicEvent(string musicId)
            => MusicId = musicId;
    }
}