using System.Collections;
using UnityEngine;

namespace Managers.SoundManager
{
    public class DisableAfterFinish : MonoBehaviour
    {
        private AudioSource _source;
        private Transform _transform;

        private void Awake()
        {
            _source   = GetComponent<AudioSource>();
            _transform = GetComponent<Transform>();
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (_disable != null)
            {
                StopCoroutine(_disable);
                _disable = null;
            }

            var time = _source && _source.clip ? _source.clip.length : 0f;
            StartCoroutine(_disable = Disable(time));
        }

        private void OnDisable()
        {
            if (_disable != null)
            {
                StopCoroutine(_disable);
                _disable = null;
            }
        }

        private IEnumerator _disable;
        public IEnumerator Disable(float time)
        {
            yield return new WaitForSeconds(time + 0.3f);
            _source.volume                = 0;
            _source.clip                  = null;
            _source.outputAudioMixerGroup = null;
            _transform.position            = Vector3.one * 1000f;
            gameObject.SetActive(false);
        }
    }
}
