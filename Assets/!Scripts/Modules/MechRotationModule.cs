using UnityEngine;

public class MechRotationModule : MechSystem
{
    [SerializeField] private MechRotationSystemSO data;
    [SerializeField] private SkinnedMeshRenderer rotationJoystickSkinnedMesh;
    [SerializeField] private Transform handTarget;

    private float xRotation;
    private float yRotation;
    private bool isControllingMech = false;
    private Quaternion mechTargetRotation;
    private float previousYRotation;

    private Vector3 turnLeftHandTargetPosition;
    private Vector3 turnRightHandTargetPosition;
    private Vector3 middleHandTargetPosition;

    private void Start()
    {
        turnLeftHandTargetPosition = new Vector3(-0.0348000005f, 0.055399999f, 0);
        turnRightHandTargetPosition = new Vector3(0.0324000008f, 0.055399999f, 0);
        middleHandTargetPosition = new Vector3(0, 0.0649999976f, 0);
    }

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

            float currentYRotation = transform.eulerAngles.y;

            transform.rotation = Quaternion.Lerp(transform.rotation, mechTargetRotation, Time.deltaTime * dynamicTurnSpeed);
            float rotationDelta = Mathf.DeltaAngle(previousYRotation, currentYRotation);

            previousYRotation = currentYRotation;


            if (rotationDelta > 0)
            {
                rotationJoystickSkinnedMesh.SetBlendShapeWeight(0, 0);
                rotationJoystickSkinnedMesh.SetBlendShapeWeight(1, 100);
                handTarget.localPosition = turnRightHandTargetPosition;
            }
            else if (rotationDelta < 0)
            {
                rotationJoystickSkinnedMesh.SetBlendShapeWeight(0, 100);
                rotationJoystickSkinnedMesh.SetBlendShapeWeight(1, 0);
                handTarget.localPosition = turnLeftHandTargetPosition;
            }
            else
            {
                rotationJoystickSkinnedMesh.SetBlendShapeWeight(0, 0);
                rotationJoystickSkinnedMesh.SetBlendShapeWeight(1, 0);
                handTarget.localPosition = middleHandTargetPosition;
            }
        }
    }

    public override void SetExecuteSolution(bool value)
    {
        isControllingMech = value;
    }
}