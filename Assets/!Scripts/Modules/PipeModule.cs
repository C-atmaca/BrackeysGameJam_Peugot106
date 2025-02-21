using UnityEngine;

public class PipeModule : TriggerOnceSystem
{
    private bool executeSolution = false;

    protected override void Update()
    {
        if (executeSolution)
        {
            SetTriggered(false);
            return;
        }


        base.Update();
    }
    public override void SetExecuteSolution(bool value)
    {
        executeSolution = value;
    }
}
