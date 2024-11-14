using Core.Services.ResourceProvider;
using Shared.ExtensionTools;
using UnityEngine;

namespace Core.Services.SoundService
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Configurations/Audio and Music/Create Sound", order = 99)]
    public class SoundConfiguration : UniqueScriptableObject, IResource
    {
        public SoundTitle SoundTitle => _soundTitle;
        public SoundType SoundType => _soundType;
        public AudioClip AudioClip => _audioClip;
        public bool PlayOnEnable => _playOnEnable;
        public bool Loop => _loop;
        public float Volume => _volume;
        public float Pitch => _pitch;
        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;

        [SerializeField] private SoundTitle _soundTitle;
        [SerializeField] private SoundType _soundType;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private bool _playOnEnable;
        [SerializeField] private bool _loop;
        [SerializeField] [Range(0, 1)] private float _volume = 1;
        [SerializeField] [Range(-3, 3)] private float _pitch = 1;
        [SerializeField] [Range(0, 15)] private float _minDistance = 0.1f;
        [SerializeField] [Range(0, 500)] private float _maxDistance = 15;
    }
}