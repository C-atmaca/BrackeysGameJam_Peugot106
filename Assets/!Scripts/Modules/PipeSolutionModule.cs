using UnityEngine;

public class PipeSolutionModule : SolutionSystem
{
    private void Update()
    {
        
    }

    public override void StartExecuteSolution()
    {
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        system.SetExecuteSolution(false);
    } 
}
