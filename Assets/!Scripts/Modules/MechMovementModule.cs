using UnityEngine;

public class MechMovementModule : MechSystem
{
    [SerializeField] private MechMovementSystemSO data;
    [SerializeField] public Transform speedJoystick;

    [SerializeField] private int currentMoveSpeedLevel = 0;
    [SerializeField] private float targetMoveSpeed;
    [SerializeField] private float currentMoveSpeed;

    [SerializeField] private Transform mechBody;

    [SerializeField] private float rotationAmount = 3f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] public float verticalSwayAmount = 3f;
    [SerializeField] public float verticalSwaySpeed = 2f;

    private bool handbrakeOnOff = false;

    private bool isSoundOn = false;

    private void Update()
    {
        if (handbrakeOnOff)
        {
            currentMoveSpeedLevel = 0;
            targetMoveSpeed = data.MaxSpeedForMoveSpeedLevels[currentMoveSpeedLevel];
            isSoundOn = false;
        }

        float joystickRotation = Mathf.Lerp(-40f, 40f, (float)currentMoveSpeedLevel / 3);
        speedJoystick.localRotation = Quaternion.Euler(joystickRotation, 0, 0);

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
            isSoundOn = true;
            transform.Translate(currentMoveSpeed * Time.deltaTime * Vector3.forward);

            float rotationValue = Mathf.PingPong(Time.time * rotationSpeed, rotationAmount * 2) - rotationAmount;

            float verticalSway = Mathf.Lerp(-verticalSwayAmount, verticalSwayAmount, Mathf.Abs(rotationValue) / rotationAmount);

            Vector3 currentRotation = mechBody.localRotation.eulerAngles;
            Vector3 currentPosition = mechBody.localPosition;

            mechBody.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, rotationValue);
            mechBody.localPosition = new Vector3(currentPosition.x, currentPosition.y + verticalSway, currentPosition.z);
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

    public void SwitchHandbreak(bool handbreak)
    {
        handbrakeOnOff = handbreak;
    }
}