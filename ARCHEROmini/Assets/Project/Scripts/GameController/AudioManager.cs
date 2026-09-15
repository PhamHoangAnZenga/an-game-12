using UnityEngine;

public class AudioManager : MySingleton<AudioManager>
{
    [SerializeField] AudioSource _audioSource;

    public void PlayShotAudio(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }

    public void Play()
    {
        _audioSource.Play();
    }
    
    public void Pause()
    {
        _audioSource.Pause();
    }
}
