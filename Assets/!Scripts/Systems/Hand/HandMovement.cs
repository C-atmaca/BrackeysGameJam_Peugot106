using DG.Tweening;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private Transform leftHandOriginalTransform;
    [SerializeField] private Transform rightHandOriginalTransform;
    [SerializeField] private Transform leftHandCurrentTargetTransform;
    [SerializeField] private Transform rightHandCurrentTargetTransform;
    [SerializeField] private RaycastFromCamera raycastFromCamera;
    [SerializeField] private Camera playerCamera;
    private Transform handTargetTransform;

    private bool fire1 = false;
    private bool fire2 = false;


    private void LateUpdate()
    {
        LeftHandMove(handTargetTransform);
        RightHandMove(handTargetTransform);


        if (HandManager.Instance.LeftHandSolutionSystem == null)
        {
            LeftHandOriginalPos();
        }
        if (HandManager.Instance.RightHandSolutionSystem == null)
        {
            RightHandOriginalPos();
        }
    }

    private void LeftHandMove(Transform targetTransform)
    {
        leftHandCurrentTargetTransform.DOMove(targetTransform.position, .5f); 
    }

    private void RightHandMove(Transform targetTransform)
    {
        rightHandCurrentTargetTransform.DOMove(targetTransform.position, .5f);
    }

    private void LeftHandOriginalPos()
    {
        leftHandCurrentTargetTransform.DOMove(leftHandOriginalTransform.position, .5f);
    }

    private void RightHandOriginalPos()
    {
        rightHandCurrentTargetTransform.DOMove(rightHandOriginalTransform.position, .5f);
    }

    public void SetTargetTransform(Transform target)
    {
        handTargetTransform = target;
    }


}
