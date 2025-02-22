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
        }
    }

    public override void SetExecuteSolution(bool value)
    {
        isControllingMech = value;
    }
}