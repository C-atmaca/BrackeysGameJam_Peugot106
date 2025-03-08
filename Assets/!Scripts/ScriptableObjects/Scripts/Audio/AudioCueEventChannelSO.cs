using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AudioCueEventChannelSO", menuName = "Scriptable Objects/AudioCueEventChannelSO")]
public class AudioCueEventChannelSO : ScriptableObject
{
    public UnityAction<AudioCueSO, AudioConfigurationSO, Vector3> OnAudioCueRequested;

    public void RaiseEvent(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 positionInSpace)
	{
		if (OnAudioCueRequested != null)
		{
			OnAudioCueRequested.Invoke(audioCue, audioConfiguration, positionInSpace);
		}
		else
		{
			Debug.LogWarning("An AudioCue was requested, but nobody picked it up. " +
				"Check why there is no AudioManager already loaded, " +
				"and make sure it's listening on this AudioCue Event channel.");
		}
	}
    
}
