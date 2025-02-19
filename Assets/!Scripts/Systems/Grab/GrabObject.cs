using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Transform targetObjectTransform;
    private Transform grabPos;


    private void Update() 
    {
        transform.position = grabPos.position;
    } 

    public void SetGrabTransform(Transform target)
    {
        grabPos = target;
    }
}
