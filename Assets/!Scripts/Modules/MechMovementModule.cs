using UnityEngine;

public class MechMovementModule : MechSystem
{
    [SerializeField] private MechMovementSystemSO data;

    [SerializeField] private int currentMoveSpeedLevel = 0;
    [SerializeField] private float targetMoveSpeed;
    [SerializeField] private float currentMoveSpeed;

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

        if (currentMoveSpeed > 0)
        {
            transform.Translate(Vector3.forward * currentMoveSpeed * Time.deltaTime);
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
        currentMoveSpeed = 0;
    }
}