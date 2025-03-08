using NUnit.Framework;
using UnityEngine;

public class ValveModule : ConstantlyIncreasingSystem
{
    private bool isSoundPlayed = false;
    protected override void Update()
    {
        if (currentValue >= data.DamageThresholdMin && currentValue <= data.DamageThresholdMax)
        {
            if (!isSoundPlayed)
            {
                // AudioManager.Instance.HearthBeatPlay();
                isSoundPlayed = true;
            }
            GeneralIndicatorSystem.Instance.ToggleValveErrorIndicator(true);
        }
        else
        {
            // AudioManager.Instance.HearthBeatStop();
            isSoundPlayed = false;
            GeneralIndicatorSystem.Instance.ToggleValveErrorIndicator(false);
        }

        base.Update();
    }

    public void DecreaseValueWithRotation()
    {
        ModifyCurrentValue((data.DecreaseRate * Time.deltaTime) * -1);
    }
}