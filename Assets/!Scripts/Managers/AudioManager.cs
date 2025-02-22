using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] audioClips;
    private Dictionary<AudioClip, AudioSource> activeSources = new Dictionary<AudioClip, AudioSource>();

    public void ButtonVoicePlay()
    {
        PlaySound(audioClips[0]);
    }

    public void ButtonVoiceStop()
    {
        StopSound(audioClips[0]);
    }

    public void HeatReleasePlay()
    {
        StartCoroutine(HeatReleasePlayWaiter(.1f, audioClips[1]));
    }

    public void HeatReleaseStop()
    {
        StopSound(audioClips[1], 0.5f);
    }

    private IEnumerator HeatReleasePlayWaiter(float time, AudioClip clip)
    {
        yield return new WaitForSeconds(time);
        PlaySound(clip);
    }

    public void ValveRotationPlay()
    {
        PlaySound(audioClips[2], loop: true);
    }

    public void ValveRotationStop()
    {
        StopSound(audioClips[2], 0.5f);
    }

    public void MechStepSoundPlay()
    {
        PlaySound(audioClips[3], loop: true);
    }

    public void MechStepSoundStop()
    {
        StopSound(audioClips[3]);
    }

    private void PlaySound(AudioClip clip, bool loop = false)
    {   
        if (clip == null) return;

        AudioSource newSource = new GameObject("AudioSource_" + clip.name).AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = loop;
        newSource.Play();

        activeSources[clip] = newSource;
    }

    private void StopSound(AudioClip clip, float fadeTime = 0)
    {
        if (clip == null || !activeSources.ContainsKey(clip)) return;
        
        AudioSource source = activeSources[clip];

        if (source == null) return;

        if (fadeTime > 0)
        {
            StartCoroutine(FadeOutAndDestroy(source, fadeTime));
        }
        else
        {
            Destroy(source.gameObject);
        }
        activeSources.Remove(clip);
    }

    private IEnumerator FadeOutAndDestroy(AudioSource source, float fadeTime)
    {
        float startVolume = source.volume;

        while (source.volume > 0)
        {
            source.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        Destroy(source.gameObject);
    }


}
