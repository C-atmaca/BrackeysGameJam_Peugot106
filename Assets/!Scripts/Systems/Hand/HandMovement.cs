using DG.Tweening;
using UnityEngine;

public enum HandType {None, LeftHand, RightHand};

public class HandMovement : MonoBehaviour
{
    [SerializeField] private Transform leftHandOriginalTransform;
    [SerializeField] private Transform rightHandOriginalTransform;
    [SerializeField] private Transform leftHandCurrentTargetTransform;
    [SerializeField] private Transform rightHandCurrentTargetTransform;
    [SerializeField] private RaycastFromCamera raycastFromCamera;
    [SerializeField] private Camera playerCamera;
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
                    leftHandCurrentTargetTransform.DOMove(handTargetTransform.position, .5f); 
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    leftHandCurrentTargetTransform.DOMove(leftHandOriginalTransform.position, .5f); 
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    rightHandCurrentTargetTransform.DOMove(handTargetTransform.position, .5f);
                }
                else if (Input.GetButtonUp("Fire2"))
                {
                    rightHandCurrentTargetTransform.DOMove(rightHandOriginalTransform.position, .5f);
                }
            }
        }
          
    }


}
