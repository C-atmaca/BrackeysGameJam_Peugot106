using DG.Tweening;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private Transform leftHandOriginalTransform;
    [SerializeField] private Transform rightHandOriginalTransform;
    [SerializeField] public Transform leftHandCurrentTargetTransform;
    [SerializeField] private Transform rightHandCurrentTargetTransform;
    [SerializeField] private RaycastFromCamera raycastFromCamera;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform leftHandModelTransform;
    [SerializeField] private Transform rightHandModelTransform;
    private Transform handTargetTransform;

    private bool fire1 = false;
    private bool fire2 = false;


    private void LateUpdate()
    {
        if (handTargetTransform != null)
        {
            LeftHandMove(handTargetTransform);
            RightHandMove(handTargetTransform);
            
        }


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

    public Transform GetLeftHandCurrentTransform()
    {
        return leftHandModelTransform;
    }
    public Transform GetRightHandCurrentTransform()
    {
        return rightHandModelTransform;
    }


}
