using UnityEngine;

public class MechMovementSolutionModule : SolutionSystem
{
    [SerializeField] private bool isHandbrake = false;

    private bool beingUsed = false;
    private bool handbrakeOnOff = false;

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
            handbrakeOnOff = !handbrakeOnOff;
            ((MechMovementModule)system).SwitchHandbreak(handbrakeOnOff);

            if (handbrakeOnOff)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            }
            return;
        }

        beingUsed = true;
    }

    public override void StopExecuteSolution()
    {
        beingUsed = false;
    }
}
