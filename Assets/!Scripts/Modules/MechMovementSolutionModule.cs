using UnityEngine;

public class MechMovementSolutionModule : SolutionSystem
{
    public override void StartExecuteSolution()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ((MechMovementModule)system).IncreaseMovementLevel();
        }
    }

    public override void StopExecuteSolution()
    {

    }
}
