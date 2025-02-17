using DG.Tweening;
using UnityEngine;

public enum HandType {None, LeftHand, RightHand};

public class HandMovement : MonoBehaviour
{
    [SerializeField] private Transform[] handOriginTransforms;
    [SerializeField] private Transform[] handCurrentTargetTransforms;
    [SerializeField] private RaycastFromCamera raycastFromCamera;
    [SerializeField] private Camera playerCamera;
    
    private HandType handType;
    private Transform handTargetTransform;


    private void Update() 
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) 
        {
            if (hit.collider.CompareTag("ActionObject"))
            {
                handTargetTransform = hit.collider.transform;

                if (Input.GetButtonDown("Fire1"))
                {
                    handType = HandType.LeftHand;
                    ChangeHandPosition(handTargetTransform);
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    ChangeHandPosition(handOriginTransforms[0]);
                    handType = HandType.None;
                }
                if (Input.GetButton("Fire2"))
                {
                    handType = HandType.RightHand;
                    ChangeHandPosition(handTargetTransform);
                }
                else if (Input.GetButtonUp("Fire2"))
                {
                    ChangeHandPosition(handOriginTransforms[1]);
                    handType = HandType.None;
                }
            }
        }
          
    }

    private void ChangeHandPosition(Transform targetTransform)
    {
        switch (handType)
        {
            case HandType.LeftHand:
                handCurrentTargetTransforms[0].DOMove(targetTransform.position, 2f);         
            break;
            case HandType.RightHand:
                handCurrentTargetTransforms[1].DOMove(targetTransform.position, 2f); 
            break;
        }
    }
}
