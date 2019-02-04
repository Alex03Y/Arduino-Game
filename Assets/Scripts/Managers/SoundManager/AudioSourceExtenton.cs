using UnityEngine;

namespace Managers.SoundManager
{
    public static class AudioSourceExtenton
    {
        public static void ApplySoundToAudioSource(this AudioSource source, Sound sound)
        {
            if (!source || sound.clip == null || sound.clip.Length == 0) return;

            source.clip                  = sound.clip[Random.Range(0, sound.clip.Length)];
            source.outputAudioMixerGroup = sound.mixerGroup;
            source.volume                = Random.Range(sound.volume.x, sound.volume.y);
            source.pitch                 = Random.Range(sound.pitch.x, sound.pitch.y);
            source.spatialBlend          = sound.is3D ? 1f : 0f;
            source.spread                = sound.is3D ? -360f : 0f;

            if (sound.is3D)
            {
                source.minDistance = sound.Distance.x;
                source.maxDistance = sound.Distance.y;
                source.SetCustomCurve(AudioSourceCurveType.CustomRolloff, sound.VolumeChange);    
            }
        }
    }
}