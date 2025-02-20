using UnityEngine;

[CreateAssetMenu(fileName = "MechMovementSystemSO", menuName = "Scriptable Objects/MechMovementSystemSO")]
public class MechMovementSystemSO : ScriptableObject
{
    public int[] MaxSpeedForMoveSpeedLevels;
    public float Acceleration;
    public float Deceleration;
}