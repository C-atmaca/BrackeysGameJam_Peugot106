using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    private AudioSource _audioSource;

    public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;

    private void Awake() 
    {
        _audioSource = this.GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;    
    }

    public void PlayAudioClip(AudioClip clip, AudioConfigurationSO settings, bool hasToLoop, Vector3 position = default)
    {
        _audioSource.clip = clip;
        settings.ApplyTo(_audioSource);
        _audioSource.transform.position = position;
        _audioSource.loop = hasToLoop;
        _audioSource.Play();

        if (!hasToLoop)
        {
            StartCoroutine(FinishedPlaying(clip.length));
        }
    }

    public void Resume()
    {
        _audioSource.Play();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public bool IsInUse()
    {
        return _audioSource.isPlaying;
    }

    public bool IsLooping()
    {
        return _audioSource.loop;
    }

    IEnumerator FinishedPlaying(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        OnSoundFinishedPlaying.Invoke(this);
    }
}
