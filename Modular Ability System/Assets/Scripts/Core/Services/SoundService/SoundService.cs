using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.ResourceProvider;
using DG.Tweening;
using Shared.Constants;
using Shared.Extensions.Rx;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Sequence = DG.Tweening.Sequence;

namespace Core.Services.SoundService
{
    public class SoundService : Shared.Extensions.Singleton<SoundService>
    {
        public ISoundMixer SoundMixer => _soundMixer;
        
        private SoundMixer _soundMixer;
        private AudioSource _audioPlayer;
        private AudioSource _musicPlayer;
        private List<AudioSource> _audioSources;
        private Dictionary<SoundTitle, SoundConfiguration> _soundConfigs;

        private Queue<AudioSource> _queueAudioSources;
        private List<AudioClip> _playedAudioClips;

        private ReactiveProperty<bool> _initialized;
        
        private void Awake()
        {
            _soundMixer = new SoundMixer();
            
            var resourceProviderService = new ResourceProviderService();
            _soundConfigs = resourceProviderService.LoadResources<SoundConfiguration>().ToDictionary(configuration => configuration.SoundTitle);
            _audioSources = new List<AudioSource>();

            _initialized = new ReactiveProperty<bool>();
            _queueAudioSources = new Queue<AudioSource>();
            _playedAudioClips = new List<AudioClip>();
            
            _musicPlayer = new GameObject($"Music Player").AddComponent<AudioSource>();
            _audioPlayer = new GameObject($"Audio Player").AddComponent<AudioSource>();
            
            _musicPlayer.transform.SetParent(transform);
            _audioPlayer.transform.SetParent(transform);

            _musicPlayer.outputAudioMixerGroup = _soundMixer.MusicGroup;
            _audioPlayer.outputAudioMixerGroup = _soundMixer.SoundGroup;

            _initialized.Subscribe(PlayQueueOfAudio);
            
            DontDestroyOnLoad(this);
        }

        public void Initialize(AudioListener audioListener)
        {
            _initialized.Value = audioListener != null;
        }

        private void Update()
        {
            if (_initialized.Value == false)
                return;
            
            if (GetRemainingTime(_musicPlayer) <= 0.1f)
            {
                PlayRandomMusic();
            }
        }

        public void PlayRandomMusic()
        {
            if (_playedAudioClips.Count == 6)
                _playedAudioClips.Clear();

            var firstMusic = Convert.ToInt32(SoundTitle.FirstMusic);
            var lastMusic = Convert.ToInt32(SoundTitle.LastMusic) + 1;
            
            var randomMusic = (SoundTitle)Random.Range(firstMusic, lastMusic);
            var config = _soundConfigs.First(item => item.Key == randomMusic).Value;
            var foundMusic = _playedAudioClips.FirstOrDefault(item => item == config.AudioClip);
            
            while (foundMusic != null)
            {
                randomMusic = (SoundTitle)Random.Range(firstMusic, lastMusic);
                config = _soundConfigs.First(item => item.Key == randomMusic).Value;
                foundMusic = _playedAudioClips.FirstOrDefault(item => item == config.AudioClip);
            }
            
            Play(randomMusic);
        }

        public void Play(SoundTitle soundTitle, bool immediate = true)
        {
            var configuration = _soundConfigs.First(config => config.Key == soundTitle);
            PlayOnPlayer(configuration.Value, immediate);
        }

        public void Play(SoundTitle soundTitle, Component target)
        {
            var configuration = _soundConfigs.First(config => config.Key == soundTitle);
            var audioSource = _audioSources.FirstOrDefault(source => source.clip == configuration.Value.AudioClip);
            if (audioSource != null)
            {
                if (GetRemainingTime(audioSource) > 0)
                {
                    audioSource.UnPause();
                }
                else
                {
                    audioSource.Play();
                }
                return;
            }
            
            if (target == null)
            {
                PlayOnNewTarget(soundTitle, configuration.Value);
                return;
            }
            
            if (target.GetComponent<AudioSource>()) return;
            PlayNewInstance(target, configuration.Value);
        }
        
        public void Pause(SoundTitle soundTitle)
        {
            var configuration = _soundConfigs.First(config => config.Key == soundTitle);
            var audioSource = _audioSources.FirstOrDefault(source => source.clip == configuration.Value.AudioClip);
            
            audioSource?.Pause();
        }

        public void TurnUpMusicVolume()
        {
            if (_musicPlayer.clip == null)
                return;
            
            var configuration = _soundConfigs.First(config => config.Value.AudioClip == _musicPlayer.clip);
            ChangeClipVolume(_musicPlayer, configuration.Value.Volume);
        }

        public void MuffleMusic()
        {
            if (_musicPlayer.clip == null)
                return;

            var configuration = _soundConfigs.First(config => config.Value.AudioClip == _musicPlayer.clip).Value;
            ChangeClipVolume(_musicPlayer, configuration.Volume / 2);
        }

        public void StopMusic()
        {
            var configuration = _soundConfigs.First(config => config.Value.SoundType == SoundType.Music);
            var audioSource = _audioSources.FirstOrDefault(source => source.clip == configuration.Value.AudioClip);
            
            audioSource?.Stop();
        }

        private void PlayOnPlayer(SoundConfiguration configuration, bool immediate)
        {
            if (_initialized.Value == false)
                return;
            
            switch (configuration.SoundType)
            {
                case SoundType.Music:
                    if (immediate)
                    {
                        SetupAudioSource(_musicPlayer, configuration, false);
                        TransitionSoundTo(_musicPlayer, configuration.AudioClip, configuration.Volume);
                        _playedAudioClips.Add(configuration.AudioClip);
                    }
                    return;
                case SoundType.Audio:
                    if (immediate)
                    {
                        _audioPlayer.Stop();
                        
                        SetupAudioSource(_audioPlayer, configuration, false);
                        _audioPlayer.clip = configuration.AudioClip;
                        _audioPlayer.Play();
                    }
                    return;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PlayNewInstance(Component target, SoundConfiguration configuration)
        {
            var audioSource = target.AddComponent<AudioSource>();
            SetupAudioSource(audioSource, configuration, true);
            audioSource.clip = configuration.AudioClip;

            if (_initialized.Value == false)
                _queueAudioSources.Enqueue(audioSource);
            else
                audioSource.Play();

            _audioSources.Add(audioSource);
        }

        private void PlayOnNewTarget(SoundTitle soundTitle, SoundConfiguration configuration)
        {
            var emptyObject = new GameObject($"{soundTitle.ToString()} audioSource");
            var audioSource = emptyObject.AddComponent<AudioSource>();
            SetupAudioSource(audioSource, configuration, true);

            if (_initialized.Value == false)
                _queueAudioSources.Enqueue(audioSource);
            else
                _audioPlayer.Play();
            
            _audioSources.Add(audioSource);
        }

        private Tween ChangeClipVolume(AudioSource audioSource, float desiredVolume) =>
            DOVirtual.Float(audioSource.volume, desiredVolume, AnimationConstants.Half,
                value => audioSource.volume = value)
                .SetUpdate(true);

        private Sequence TransitionSoundTo(AudioSource audioSource, AudioClip audioClip, float desiredVolume)
        {
            var sequence = DOTween.Sequence();
            sequence
                .Append(DOVirtual.Float(audioSource.volume, desiredVolume, AnimationConstants.Half,
                    value => audioSource.volume = value)
                .OnComplete(audioSource.Stop))
                .SetUpdate(true);

            audioSource.clip = audioClip;

            sequence
                .AppendCallback(audioSource.Play)
                .Append(DOVirtual.Float(audioSource.volume, desiredVolume, AnimationConstants.Half,
                    value => audioSource.volume = value))
                .SetUpdate(true);
            
            return sequence;
        }

        private void PlayQueueOfAudio(bool initialized)
        {
            if (initialized == false)
                return;
            
            for (var i = 0; i < _queueAudioSources.Count; i++)
            {
                if (_queueAudioSources.TryDequeue(out AudioSource audioSource))
                {
                    audioSource.Play();
                }
            }
        }

        private void SetupAudioSource(AudioSource audioSource, SoundConfiguration configuration, bool spatialSound)
        {
            audioSource.playOnAwake = configuration.PlayOnEnable;
            audioSource.loop = configuration.Loop;
            audioSource.volume = configuration.Volume;
            audioSource.pitch = configuration.Pitch;
            audioSource.spatialBlend = spatialSound ? 1 : 0;
            audioSource.minDistance = configuration.MinDistance;
            audioSource.maxDistance = configuration.MaxDistance;
            
            audioSource.outputAudioMixerGroup = configuration.SoundType == SoundType.Audio 
                ? _soundMixer.SoundGroup 
                : _soundMixer.MusicGroup;
        }

        private double GetRemainingTime(AudioSource audioSource)
        {
            if (audioSource.clip == null)
                return 0.1f;
            
            double timeRemainingOnMainClip = (audioSource.clip.length - audioSource.time) / audioSource.pitch;
            return AudioSettings.dspTime + timeRemainingOnMainClip;
        }

        private void OnDestroy()
        {
            _soundMixer.Dispose();
        }
    } 
}