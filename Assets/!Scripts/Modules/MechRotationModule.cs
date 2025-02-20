using UnityEngine;

public class MechRotationModule : MechSystem
{
    [SerializeField] private MechRotationSystemSO data;
    [SerializeField] private Transform mechFrontWindow;

    private float xRotation;
    private float yRotation;
    private bool isControllingMech = false;

    private void Update()
    {
        if (isControllingMech)
        {
            PlayerCam.Instance.OverrideCamera(mechFrontWindow);

            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * data.LookSensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * data.LookSensitivityY;

            yRotation += mouseX;
            yRotation = Mathf.Clamp(yRotation, -data.YRotationLimit, data.YRotationLimit);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -data.XRotationLimit, data.XRotationLimit);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            PlayerCam.Instance.stopPlayerRotation = true;
        }
        else
        {
            PlayerCam.Instance.stopPlayerRotation = false;
        }
    }

    public override void SetExecuteSolution(bool value)
    {
        isControllingMech = value;
    }
}