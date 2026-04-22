using UnityEngine;

namespace MyGame.Gameplay
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        private AudioSource _audioSource;

        void Awake()
        {
            // 確保場景中只有一個 AudioManager
            if (Instance == null)
            {
                Instance = this;
                _audioSource = GetComponent<AudioSource>();
                DontDestroyOnLoad(gameObject); // 關鍵：跨場景不毀滅
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void PlayEffect(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }
        public void Play(AudioClip clip)
        {
            _audioSource.clip = clip;
            _audioSource.loop = true;
            _audioSource.Play();
        }
        public void Stop()
        {
            _audioSource.loop = false;
            _audioSource.Stop();
        }
    }
}