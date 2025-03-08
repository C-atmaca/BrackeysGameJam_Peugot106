using UnityEngine;

[CreateAssetMenu(fileName = "NewLocation", menuName = "Scene Data/LocationSO")]
public class LocationSO : GameSceneSO
{
    [Header("Location Specific")]
    public string locationName;
}
