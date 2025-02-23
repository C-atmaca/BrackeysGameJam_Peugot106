using UnityEngine;

public class MechMovementModule : MechSystem
{
    [SerializeField] private MechMovementSystemSO data;
    [SerializeField] public Transform speedJoystick;

    [SerializeField] private int currentMoveSpeedLevel = 0;
    [SerializeField] private float targetMoveSpeed;
    [SerializeField] private float currentMoveSpeed;

    [SerializeField] private Transform mechBody;
    [SerializeField] private Transform raycastOrigin;

    [SerializeField] private float rotationAmount = 3f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] public float verticalSwayAmount = 3f;
    [SerializeField] public float verticalSwaySpeed = 2f;

    private bool handbrakeOnOff = false;

    private bool isMechMoving = false;
    [SerializeField] private LayerMask stopMask;

    private void Update()
    {
        if (LevelManager.Instance.GetPause()) return;

        if (handbrakeOnOff)
        {
            currentMoveSpeedLevel = 0;
            targetMoveSpeed = data.MaxSpeedForMoveSpeedLevels[currentMoveSpeedLevel];

            if (isMechMoving)
            {
                AudioManager.Instance.MechStepSoundStop();
                isMechMoving = false;
            }
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
            if (!isMechMoving)
            {
                AudioManager.Instance.MechStepSoundPlay();
                isMechMoving = true;
            }

            float moveDistance = currentMoveSpeed * Time.deltaTime;
            Vector3 moveDirection = transform.forward * moveDistance;

            Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * (moveDistance + 0.2f), Color.green);
            if (!Physics.SphereCast(raycastOrigin.position, 1f, raycastOrigin.forward, out _, moveDistance + 0.2f, stopMask))
            {
                transform.Translate(moveDirection, Space.World);
            }

            float rotationValue = Mathf.PingPong(Time.time * rotationSpeed, rotationAmount * 2) - rotationAmount;
            float verticalSway = Mathf.Lerp(-verticalSwayAmount, verticalSwayAmount, Mathf.Abs(rotationValue) / rotationAmount);

            Vector3 currentRotation = mechBody.localRotation.eulerAngles;
            Vector3 currentPosition = mechBody.localPosition;

            mechBody.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, rotationValue);
            mechBody.localPosition = new Vector3(currentPosition.x, currentPosition.y + verticalSway, currentPosition.z);
        }
        else if (isMechMoving)
        {
            AudioManager.Instance.MechStepSoundStop();
            isMechMoving = false;
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