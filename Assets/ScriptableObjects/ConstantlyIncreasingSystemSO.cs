using UnityEngine;

[CreateAssetMenu(fileName = "ConstantlyIncreasingSystemSO", menuName = "Scriptable Objects/ConstantlyIncreasingSystemSO")]
public class ConstantlyIncreasingSystemSO : ScriptableObject
{
    public float IncreaseRate;
    public float DecreaseRate;
}