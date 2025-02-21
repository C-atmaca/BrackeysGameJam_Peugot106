using UnityEngine;

public class MiningSolutionModule : SolutionSystem
{
    public override void StartExecuteSolution()
    {
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        system.SetExecuteSolution(false);
    }
}