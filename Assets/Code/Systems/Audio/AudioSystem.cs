using System.Collections.Generic;
using Game.Core;
using R3;
using UnityEngine;
using VContainer;
namespace Game.Systems.Audio
{
    public class AudioSystem : IAudioSystem, IInitializable 
    {
        [Inject] private EventBus _eventBus;
        private readonly Dictionary<string, AudioClip> _sfxClips;
        private readonly Dictionary<string, AudioClip> _musicClips;

        private AudioSource _sfxSource;
        private AudioSource _musicSource;

        public AudioSystem() {
            Debug.Log("AudioSystem is starting.");
            
            _sfxClips = new Dictionary<string, AudioClip>();
            _musicClips = new Dictionary<string, AudioClip>();
        }
        public void Initialize() {
            var audioRoot = new GameObject("AudioSystem");
            Object.DontDestroyOnLoad(audioRoot);

            _sfxSource = audioRoot.AddComponent<AudioSource>();
            _musicSource = audioRoot.AddComponent<AudioSource>();
            _musicSource.loop = true;

            _eventBus.GetSubject<PlaySfxEvent>().Subscribe(OnPlaySfx);
            _eventBus.GetSubject<PlayMusicEvent>().Subscribe(OnPlayMusic);
            _eventBus.GetSubject<StopMusicEvent>().Subscribe(OnStopMusic);
        }
        public void PlaySfx(string soundId) 
        {
            if (_sfxClips.TryGetValue(soundId, out var clip)) 
                _sfxSource.PlayOneShot(clip);
        }
        public void PlayMusic(string musicId) {
            if (_musicClips.TryGetValue(musicId, out var clip)) 
            {
                _musicSource.clip = clip;
                _musicSource.Play();
            }
        }
        public void StopMusic() {
            _musicSource.Stop();
        }
        private void OnPlaySfx(PlaySfxEvent evt) {
            PlaySfx(evt.SoundId);
        }
        private void OnPlayMusic(PlayMusicEvent evt) {
            PlayMusic(evt.MusicId);
        }
        private void OnStopMusic(StopMusicEvent evt) {
            StopMusic();
        }
        public void AddSfx(string soundId, AudioClip clip) 
        {
            if (!_sfxClips.ContainsKey(soundId)) 
            {
                _sfxClips.Add(soundId, clip);
            }
            else 
            {
                Debug.LogWarning($"SFX with ID '{soundId}' already exists.");
            }
        }

        public void AddMusic(string musicId, AudioClip clip) 
        {
            if (!_musicClips.ContainsKey(musicId)) 
            {
                _musicClips.Add(musicId, clip);
            }
            else 
            {
                Debug.LogWarning($"Music with ID '{musicId}' already exists.");
            }
        }
    }
}