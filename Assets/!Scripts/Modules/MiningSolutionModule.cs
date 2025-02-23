using UnityEngine;

public class MiningSolutionModule : SolutionSystem
{
    public override void StartExecuteSolution()
    {
        AudioManager.Instance.MiningLaserPlay();
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        AudioManager.Instance.MiningLaserStop();
        system.SetExecuteSolution(false);
    }
}