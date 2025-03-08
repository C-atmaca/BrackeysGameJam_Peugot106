using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "LoadEventChannelSO", menuName = "Scriptable Objects/LoadEventChannelSO")]
public class LoadEventChannelSO : ScriptableObject
{
    public UnityAction<GameSceneSO[], bool> OnLoadingRequested;

	public void RaiseEvent(GameSceneSO[] locationsToLoad, bool showLoadingScreen)
	{
		if (OnLoadingRequested != null)
		{
			OnLoadingRequested.Invoke(locationsToLoad, showLoadingScreen);
		}
		else
		{
			Debug.LogWarning("A Scene loading was requested, but nobody picked it up." +
				"Check why there is no SceneLoader already present, " +
				"and make sure it's listening on this Load Event channel.");
		}
	}
}
