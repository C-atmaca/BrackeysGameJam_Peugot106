using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioSource source;
    public AudioClip[] audioClips;

    public void ButtonVoice()
    {
        source.clip = audioClips[0];
        source.Play();
    }
}
