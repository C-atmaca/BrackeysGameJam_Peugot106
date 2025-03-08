using UnityEngine;

public class MiningSolutionModule : SolutionSystem
{
    public override void StartExecuteSolution()
    {
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        // AudioManager.Instance.MiningLaserStop();
        ((MiningModule)system).isSongPlayed = false;
        system.SetExecuteSolution(false);
    }
}