using UnityEngine;

[CreateAssetMenu(fileName = "ConstantlyIncreasingSystemSO", menuName = "Scriptable Objects/ConstantlyIncreasingSystemSO")]
public class ConstantlyIncreasingSystemSO : ScriptableObject
{
    public float IncreaseRateMinInclusive;
    public float IncreaseRateMaxExclusive;
    public float DecreaseRate;
    public float DamageStartDelay;
    public int DamagePerInterval;
    public float DamageInterval;
    public float DamageThresholdMin;
    public float DamageThresholdMax;
}