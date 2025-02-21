using DG.Tweening;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.XR;

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

    private void Update()
    {
        if (HandManager.Instance.LeftHandSolutionSystem != null)
        {
            if (HandManager.Instance.LeftHandSolutionSystem.activateWithObject)
            {
                if (HandManager.Instance.LeftHandGrabbedObject != null)
                {
                    if (HandManager.Instance.LeftHandGrabbedObject == HandManager.Instance.LeftHandSolutionSystem.grabObject)
                    {
                        LeftHandMove(HandManager.Instance.LeftHandSolutionSystem.handPlacementTarget);
                    }
                }
            }
            else
            {
                LeftHandMove(HandManager.Instance.LeftHandSolutionSystem.handPlacementTarget);
            }
        }
        if (HandManager.Instance.RightHandSolutionSystem != null)
        {
            if (HandManager.Instance.RightHandSolutionSystem.activateWithObject)
            {
                if (HandManager.Instance.RightHandGrabbedObject != null)
                {
                    if (HandManager.Instance.RightHandGrabbedObject == HandManager.Instance.RightHandSolutionSystem.grabObject)
                    {
                        RightHandMove(HandManager.Instance.RightHandSolutionSystem.handPlacementTarget);
                    }
                }
            }
            else
            {
                RightHandMove(HandManager.Instance.RightHandSolutionSystem.handPlacementTarget);
            }
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
        leftHandCurrentTargetTransform.DOMove(targetTransform.position, 0.1f);
    }

    private void RightHandMove(Transform targetTransform)
    {
        rightHandCurrentTargetTransform.DOMove(targetTransform.position, 0.1f);
    }

    private void LeftHandOriginalPos()
    {
        leftHandCurrentTargetTransform.DOMove(leftHandOriginalTransform.position, 0.1f);
    }

    private void RightHandOriginalPos()
    {
        rightHandCurrentTargetTransform.DOMove(rightHandOriginalTransform.position, 0.1f);
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