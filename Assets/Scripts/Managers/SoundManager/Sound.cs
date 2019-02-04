using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Managers.SoundManager
{
    [Serializable]
    public struct Sound
    {
        public AudioClip[]     clip;
        public AudioMixerGroup mixerGroup;
        public Vector2         pitch;
        public Vector2         volume;
        public bool            is3D;
        public Vector2         Distance;
        public AnimationCurve  VolumeChange;

        [ContextMenu("Clear Data")]
        public void Clear()
        {
            clip       = new AudioClip[0];
            is3D       = true;
            mixerGroup = null;
        }

        [ContextMenu("Reset To Default")]
        public void SetDefault()
        {
            pitch        = new Vector2(0.95f, 1.05f);
            volume       = new Vector2(0.95f, 1.05f);
            Distance     = new Vector2(1.5f, 10f);
            VolumeChange = new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
        }
    }
}