using UnityEngine;

public class MechMovementSolutionModule : SolutionSystem
{
    [SerializeField] private bool isHandbrake = false;

    private bool beingUsed = false;

    private void Update()
    {
        if (beingUsed)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ((MechMovementModule)system).IncreaseMovementLevel();
            }
        }
    }

    public override void StartExecuteSolution()
    {
        if (isHandbrake)
        {
            ((MechMovementModule)system).StopMech();
            return;
        }

        beingUsed = true;
    }

    public override void StopExecuteSolution()
    {
        beingUsed = false;
    }
}
