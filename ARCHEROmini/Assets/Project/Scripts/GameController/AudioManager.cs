using UnityEngine;

public class AudioManager : MySingleton<AudioManager>
{
    [SerializeField] AudioSource _audioSource;
    
    public void PlayShotAudio(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
