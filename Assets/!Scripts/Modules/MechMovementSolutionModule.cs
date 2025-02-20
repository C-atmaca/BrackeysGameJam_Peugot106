using UnityEngine;

public class MechMovementSolutionModule : SolutionSystem
{
    private bool beingUsed = false;

    private void Update()
    {
        if (beingUsed)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("W Pressed.");
                ((MechMovementModule)system).IncreaseMovementLevel();
            }
        }
    }

    public override void StartExecuteSolution()
    {
        beingUsed = true;
    }

    public override void StopExecuteSolution()
    {
        beingUsed = false;
    }
}
