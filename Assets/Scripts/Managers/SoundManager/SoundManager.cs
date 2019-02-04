using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers.SoundManager
{
    public class SoundManager : MonoBehaviour
    {
        public AudioSource       AudioSourcePrefab;
        public int               AudioSourcePoolSize;
        Queue<CustomAudioSource> _poolQueue = new Queue<CustomAudioSource>();

        [Serializable]
        public class CustomAudioSource
        {
            public AudioSource        AudioSource;
            public Transform          Transform;
            public DisableAfterFinish DisableSoundAfterFinish;

            public CustomAudioSource(AudioSource        audioSource, Transform transform,
                                     DisableAfterFinish disableSoundAfterFinish)
            {
                AudioSource             = audioSource;
                Transform               = transform;
                DisableSoundAfterFinish = disableSoundAfterFinish;
            }
        }

        private void Start()
        {
            for (var i = 0; i < AudioSourcePoolSize; i++)
            {
                var obj = Instantiate(AudioSourcePrefab, transform);
                _poolQueue.Enqueue(new CustomAudioSource(obj, obj.transform, obj.GetComponent<DisableAfterFinish>()));
            }
        }

        #region play
        public void Play(Sound sound, Vector3 position = default)
        {
            var customAudio = _poolQueue.Dequeue();
            customAudio.Transform.gameObject.SetActive(false);
            customAudio.AudioSource.ApplySoundToAudioSource(sound);
            customAudio.Transform.position = position;
            customAudio.Transform.gameObject.SetActive(true);
            customAudio.AudioSource.Play();
            _poolQueue.Enqueue(customAudio);
        }

        public CustomAudioSource PlayLoop(Sound sound)
        {
            var customAudio = _poolQueue.Dequeue();
            customAudio.DisableSoundAfterFinish.enabled = false;
            customAudio.AudioSource.ApplySoundToAudioSource(sound);
            customAudio.Transform.gameObject.SetActive(true);
            customAudio.AudioSource.Play();
            customAudio.AudioSource.loop = true;

            return customAudio;
        }

        public void StopLoop(CustomAudioSource source)
        {
            if (source == null || source.AudioSource == null) return;

            source.AudioSource.loop                = false;
            source.DisableSoundAfterFinish.enabled = true;
            source.AudioSource.Stop();
            if(gameObject.activeInHierarchy)
                StartCoroutine(source.DisableSoundAfterFinish.Disable(0f));
            _poolQueue.Enqueue(source);
        }
        #endregion

        #region singleton
        private static SoundManager _instance;
        public static SoundManager Instance() => _instance;
        private void OnEnable() => _instance = FindObjectOfType<SoundManager>();
        #endregion
    }
}