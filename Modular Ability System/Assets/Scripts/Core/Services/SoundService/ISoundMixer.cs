using System;
using Shared.Extensions.Rx;
using UnityEngine.Audio;

namespace Core.Services.SoundService
{
    public interface ISoundMixer : IDisposable
    {
        IReactiveProperty<float> MasterVolume { get; }
        IReactiveProperty<float> MusicVolume { get; }
        IReactiveProperty<float> SoundVolume { get; }
        
       AudioMixerGroup MasterGroup { get; }
       AudioMixerGroup MusicGroup { get; }
       AudioMixerGroup SoundGroup { get; }
    }
}