using Shared.Extensions.Rx;
using UnityEngine;
using UnityEngine.Audio;

namespace Core.Services.SoundService
{
    public class SoundMixer : ISoundMixer
    {
        public AudioMixer AudioMixer => _audioMixer;
        
        public IReactiveProperty<float> MasterVolume => _masterVolume;
        public IReactiveProperty<float> MusicVolume => _musicVolume;
        public IReactiveProperty<float> SoundVolume => _soundVolume;

        public AudioMixerGroup MasterGroup { get; private set; }
        public AudioMixerGroup MusicGroup { get; private set; }
        public AudioMixerGroup SoundGroup { get; private set; }
        
        private readonly AudioMixer _audioMixer;
        
        private readonly ReactiveProperty<float> _masterVolume;
        private readonly ReactiveProperty<float> _musicVolume;
        private readonly ReactiveProperty<float> _soundVolume;
        
        private const string MixerPath = "Configurations/Sound Configurations/Master";
        
        private const int MaxDecibelPower = 20;
        private const int MinDecibelPower = -80;
        
        public SoundMixer()
        {
            _audioMixer = Resources.Load<AudioMixer>(MixerPath);
            
            MasterGroup = _audioMixer.FindMatchingGroups("Master")[0];
            MusicGroup = _audioMixer.FindMatchingGroups("Music")[0];
            SoundGroup = _audioMixer.FindMatchingGroups("Sound")[0];
            
            _masterVolume = new ReactiveProperty<float>();
            _musicVolume = new ReactiveProperty<float>();
            _soundVolume = new ReactiveProperty<float>();

            _masterVolume.Subscribe(SetMasterVolume);
            _musicVolume.Subscribe(SetMusicVolume);
            _soundVolume.Subscribe(SetSoundVolume);
        }
        
        private void SetMasterVolume(float volume)
        {
            var volumeToDecibel = volume * (MaxDecibelPower - MinDecibelPower) + MinDecibelPower;
            _audioMixer.SetFloat("Master Volume", volumeToDecibel);
        }

        private void SetMusicVolume(float volume)
        {
            var volumeToDecibel = volume * (MaxDecibelPower - MinDecibelPower) + MinDecibelPower;
            _audioMixer.SetFloat("Music Volume", volumeToDecibel);
        }

        private void SetSoundVolume(float volume)
        {
            var volumeToDecibel = volume * (MaxDecibelPower - MinDecibelPower) + MinDecibelPower;
            _audioMixer.SetFloat("Sound Volume", volumeToDecibel);
        }

        public void Dispose()
        {
            // _saveLoadService.Save(_settingsSaveModel);
        }
    }
}