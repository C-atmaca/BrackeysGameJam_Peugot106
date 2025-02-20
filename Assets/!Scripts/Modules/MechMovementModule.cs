using UnityEngine;

public class MechMovementModule : MechSystem
{
    [SerializeField] private MechMovementSystemSO data;

    private int currentMoveSpeedLevel = 0;
    private float targetMoveSpeed;
    private float currentMoveSpeed;

    private void Update()
    {
        if (currentMoveSpeed < targetMoveSpeed)
        {
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, targetMoveSpeed, data.Acceleration * Time.deltaTime);
        }
        else if (currentMoveSpeed > targetMoveSpeed)
        {
            currentMoveSpeed = Mathf.MoveTowards(currentMoveSpeed, targetMoveSpeed, data.Deceleration * Time.deltaTime);
        }
    }

    public void IncreaseMovementLevel()
    {
        if (currentMoveSpeedLevel < data.MaxSpeedForMoveSpeedLevels.Length - 1)
        {
            currentMoveSpeedLevel++;
            targetMoveSpeed = data.MaxSpeedForMoveSpeedLevels[currentMoveSpeedLevel];
        }
    }

    public void StopMech()
    {
        currentMoveSpeedLevel = 0;
        targetMoveSpeed = data.MaxSpeedForMoveSpeedLevels[currentMoveSpeedLevel];
        // stop suddenly
    }
}