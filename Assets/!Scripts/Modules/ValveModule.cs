using UnityEngine;

public class ValveModule : ConstantlyIncreasingSystem
{
    protected override void Update()
    {
        base.Update();
    }

    public void DecreaseValueWithRotation()
    {
        ModifyCurrentValue((data.DecreaseRate * Time.deltaTime) * -1);
    }
}