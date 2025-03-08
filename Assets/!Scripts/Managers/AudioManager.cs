using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Sound Emitter Pool")]
    [SerializeField] private SoundEmitterFactorySO _factory = default;
    [SerializeField] private SoundEmitterPoolSO _pool = default;
    [SerializeField] private int _initialSize = 10;

    [Header("Listening on Channels")]
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play SFXs")]
    [SerializeField] private AudioCueEventChannelSO _SFXEventChannel = default;
	[Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play Music")]
	[SerializeField] private AudioCueEventChannelSO _musicEventChannel = default;
    
    [Header("Audio control")]
	[SerializeField] private AudioMixer audioMixer = default;
	[Range(0f, 1f)]
	[SerializeField] private float _masterVolume = 1f;
	[Range(0f, 1f)]
	[SerializeField] private float _musicVolume = 1f;
	[Range(0f, 1f)]
	[SerializeField] private float _sfxVolume = 1f;

    private void Awake() 
    {
        _SFXEventChannel.OnAudioCueRequested += PlayAudioCue;
		_musicEventChannel.OnAudioCueRequested += PlayAudioCue;

        _pool.Prewarm(_initialSize);
		_pool.SetParent(this.transform);   
    }

	void OnValidate()
	{
		if (Application.isPlaying)
		{
			SetGroupVolume("MasterVolume", _masterVolume);
			SetGroupVolume("MusicVolume", _musicVolume);
			SetGroupVolume("SFXVolume", _sfxVolume);
		}
	}

    public void SetGroupVolume(string parameterName, float normalizedVolume)
	{
        
        
		bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
        
		if (!volumeSet)
			Debug.LogError("The AudioMixer parameter was not found");
	}

    public float GetGroupVolume(string parameterName)
	{
		if (audioMixer.GetFloat(parameterName, out float rawVolume))
		{
			return MixerValueToNormalized(rawVolume);
		}
		else
		{
			Debug.LogError("The AudioMixer parameter was not found");
			return 0f;
		}
    }

    private float MixerValueToNormalized(float mixerValue)
	{
		return 1f + (mixerValue / 80f);
	}
	
	private float NormalizedToMixerValue(float normalizedValue)
	{
		return (normalizedValue - 1f) * 80f;
	}

    public void PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO settings, Vector3 position = default)
	{
		AudioClip[] clipsToPlay = audioCue.GetClips();
		int nOfClips = clipsToPlay.Length;

		for (int i = 0; i < nOfClips; i++)
		{
			SoundEmitter soundEmitter = _pool.Request();
			if (soundEmitter != null)
			{
				soundEmitter.PlayAudioClip(clipsToPlay[i], settings, audioCue.looping, position);
				if (!audioCue.looping)
					soundEmitter.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;
			}
		}
	}

    private void OnSoundEmitterFinishedPlaying(SoundEmitter soundEmitter)
	{
		soundEmitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
		soundEmitter.Stop();
		_pool.Return(soundEmitter);
	}
}
