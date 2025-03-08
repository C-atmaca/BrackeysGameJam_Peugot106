using UnityEngine;

public class MechRotationSolutionModule : SolutionSystem
{
    public override void StartExecuteSolution()
    {
        // AudioManager.Instance.MechRotationSoundPlay();
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        // AudioManager.Instance.MechRotationSoundStop();
        system.SetExecuteSolution(false);
    }
}