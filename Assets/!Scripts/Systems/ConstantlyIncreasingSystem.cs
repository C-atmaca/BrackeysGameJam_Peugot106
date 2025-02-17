using UnityEngine;

public class ConstantlyIncreasingSystem : MechSystem
{
    [SerializeField] protected ConstantlyIncreasingSystemSO data;

    [SerializeField] protected float currentValue;

    protected virtual void Update()
    {
        var increaseValue = data.IncreaseRate * SystemsManager.Instance.GlobalData.GlobalMultiplier * Time.deltaTime;
        ModifyCurrentValue(increaseValue);
    }

    public float GetCurrentValue()
    {
        return currentValue;
    }

    public void SetCurrentValue(float value)
    {
        currentValue = Mathf.Clamp01(value);
    }

    public void ModifyCurrentValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp01(currentValue);
    }
}