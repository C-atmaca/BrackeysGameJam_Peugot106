using System;
using UnityEngine;

public class PlayerCam : Singleton<PlayerCam>
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    public bool stopPlayerRotation = false;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;  
        Cursor.visible = false;
    }

    private void Update() 
    {
        if (stopPlayerRotation)
        {
            return;
        }

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void OverrideCamera(Transform target, float rotationSpeed = 5f)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}