using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastFromCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float rayDistance;
    [SerializeField] private HandMovement handmovement;

    private Outline lastOutlinedObject = null;



    private void FixedUpdate()
    {
        Vector3 rayOrigin = targetObject.position;
        Vector3 rayDirection = targetObject.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("ActionObject"))
            {
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green);
                
                if (hit.collider.TryGetComponent<Outline>(out Outline outline))
                {
                    if (lastOutlinedObject != outline)
                    {
                        DisableLastOutline();
                    }

                    //outline.enabled = true;
                    lastOutlinedObject = outline;
                }
                hit.collider.TryGetComponent<SolutionSystem>(out SolutionSystem solutionSystem);
                if (Input.GetButton("Fire1"))
                {
                    if (HandManager.Instance.LeftHandSolutionSystem != null || HandManager.Instance.RightHandSolutionSystem == solutionSystem) return;
                    HandManager.Instance.LeftHandSolutionSystem = solutionSystem;
                    // handmovement.SetTargetTransform(solutionSystem.handPlacementTarget);
                    solutionSystem.StartExecuteSolution();
                }
                if (Input.GetButton("Fire2"))
                {
                    if (HandManager.Instance.RightHandSolutionSystem != null || HandManager.Instance.LeftHandSolutionSystem == solutionSystem) return;
                    HandManager.Instance.RightHandSolutionSystem = solutionSystem;
                    // handmovement.SetTargetTransform(solutionSystem.handPlacementTarget);
                    solutionSystem.StartExecuteSolution();
                }
            }
            else if (hit.collider.CompareTag("PickMe"))
            {
                hit.collider.TryGetComponent<GrabObject>(out GrabObject grabObject);

                if (Input.GetButton("Fire1"))
                {
                    grabObject.SetGrabTransform(handmovement.GetLeftHandCurrentTransform());
                }
                else if (Input.GetButton("Fire2"))
                {
                    grabObject.SetGrabTransform(handmovement.GetRightHandCurrentTransform());
                }

            }
            else
            {
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
                DisableLastOutline();
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
            DisableLastOutline();
        }
    }

    private void DisableLastOutline()
    {
        if (lastOutlinedObject != null)
        {
            lastOutlinedObject.enabled = false;
            lastOutlinedObject = null;
        }
    }
}