using UnityEngine;

public class MechRotationModule : MechSystem
{
    [SerializeField] private MechRotationSystemSO data;
    [SerializeField] private Transform mechFrontWindow;
    [SerializeField] private float maxLeanAmount;

    private float xRotation;
    private float yRotation;
    private bool isControllingMech = false;
    private Quaternion mechTargetRotation;
    private float leanAmount = 0f;

    private void Update()
    {
        if (isControllingMech)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * data.LookSensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * data.LookSensitivityY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -data.XRotationLimit, data.XRotationLimit);

            //PlayerCam.Instance.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            Vector3 mechForward = transform.forward;
            Vector3 cameraForward = PlayerCam.Instance.transform.forward;

            mechForward.y = 0;
            cameraForward.y = 0;

            float angleDifference = Vector3.SignedAngle(mechForward, cameraForward, Vector3.up);

            mechTargetRotation = Quaternion.Euler(0, PlayerCam.Instance.transform.eulerAngles.y, 0);

            float turnSpeedMultiplier = Mathf.Clamp01(Mathf.Abs(angleDifference) / 90f);
            float dynamicTurnSpeed = Mathf.Lerp(0.5f, data.MechTurnSpeed, turnSpeedMultiplier);

            transform.rotation = Quaternion.Lerp(transform.rotation, mechTargetRotation, Time.deltaTime * dynamicTurnSpeed);

            // Store the target lean amount (calculate based on rotation angle)
            float targetLeanAmount = Mathf.Lerp(-maxLeanAmount, maxLeanAmount, Mathf.Abs(angleDifference) / 90f);

            // Adjust the sign of the lean amount based on the actual angle direction
            targetLeanAmount = Mathf.Sign(angleDifference) * targetLeanAmount;

            // Smoothly transition the lean value (to avoid snapping)
            leanAmount = Mathf.Lerp(leanAmount, targetLeanAmount, Time.deltaTime * 5);

            // Apply the lean effect (this can be to the mech body or a specific part of it)
            Vector3 currentLean = transform.localRotation.eulerAngles;

            // Adjust the Z rotation based on the smoothed lean amount
            transform.localRotation = Quaternion.Euler(currentLean.x, currentLean.y, leanAmount);
        }
    }

    public override void SetExecuteSolution(bool value)
    {
        isControllingMech = value;
    }
}