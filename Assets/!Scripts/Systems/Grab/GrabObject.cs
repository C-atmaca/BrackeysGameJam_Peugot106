using DG.Tweening;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    private Vector3 originalTransform;
    private Transform grabPos;
    private bool isGrabbed = false;

    private void Awake() 
    {
        originalTransform = transform.position;    
    }

    private void Update() 
    {
        if (!isGrabbed) return;
        
        if (grabPos != null)
        {
            transform.position = grabPos.position;
        }
    } 

    public void SetGrabTransform(Transform target)
    {
        isGrabbed = true;
        grabPos = target;
        transform.SetParent(target);
    }

    public void SetToOriginalPos()
    {
        isGrabbed = false;
        transform.SetParent(null);
        transform.DOMove(originalTransform, 1f);

    }
}
