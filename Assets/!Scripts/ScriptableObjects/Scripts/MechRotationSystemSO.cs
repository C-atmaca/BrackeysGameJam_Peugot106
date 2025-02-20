using UnityEngine;

[CreateAssetMenu(fileName = "MechRotationSystemSO", menuName = "Scriptable Objects/MechRotationSystemSO")]
public class MechRotationSystemSO : ScriptableObject
{
    public float LookSensitivityX;
    public float LookSensitivityY;
    public float XRotationLimit;
    public float YRotationLimit;
}