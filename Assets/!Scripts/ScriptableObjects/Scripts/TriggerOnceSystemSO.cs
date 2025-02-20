using UnityEngine;

[CreateAssetMenu(fileName = "TriggerOnceSystemSO", menuName = "Scriptable Objects/TriggerOnceSystemSO")]
public class TriggerOnceSystemSO : ScriptableObject
{
    public int TriggerTimeMinInclusive;
    public int TriggerTimeMaxExclusive;
    public float DamageStartDelay;
    public int DamagePerInterval;
    public float DamageInterval;
}