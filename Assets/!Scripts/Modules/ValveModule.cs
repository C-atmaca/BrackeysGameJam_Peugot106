using UnityEngine;

public class ValveModule : ConstantlyIncreasingSystem
{
    protected override void Update()
    {
        if (currentValue >= data.DamageThresholdMin && currentValue <= data.DamageThresholdMax)
        {
            GeneralIndicatorSystem.Instance.ToggleValveErrorIndicator(true);
        }
        else
        {
            GeneralIndicatorSystem.Instance.ToggleValveErrorIndicator(false);
        }

        base.Update();
    }

    public void DecreaseValueWithRotation()
    {
        ModifyCurrentValue((data.DecreaseRate * Time.deltaTime) * -1);
    }
}