using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class RaycastFromCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float rayDistance;
    [SerializeField] private HandMovement handmovement;

    private Outline lastOutlinedObject = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 rayOrigin = targetObject.position;
        Vector3 rayDirection = targetObject.forward;

        if (Physics.SphereCast(rayOrigin, .10f, rayDirection, out RaycastHit hit, rayDistance))
        {
            
            if (hit.collider.CompareTag("ActionObject"))
            {         
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
                    if (HandManager.Instance.LeftHandSolutionSystem == null && HandManager.Instance.RightHandSolutionSystem != solutionSystem)
                    {
                        HandManager.Instance.LeftHandSolutionSystem = solutionSystem;
                        // handmovement.SetTargetTransform(solutionSystem.handPlacementTarget);
                        solutionSystem.StartExecuteSolution();
                    }
                }
                if (Input.GetButton("Fire2"))
                {
                    if (HandManager.Instance.RightHandSolutionSystem == null && HandManager.Instance.LeftHandSolutionSystem != solutionSystem)
                    {
                        HandManager.Instance.RightHandSolutionSystem = solutionSystem;
                        // handmovement.SetTargetTransform(solutionSystem.handPlacementTarget);
                        solutionSystem.StartExecuteSolution();
                    }
                }
            }
            else if (hit.collider.CompareTag("PickMe"))
            {
                hit.collider.TryGetComponent<GrabObject>(out GrabObject grabObject);
                
                if (Input.GetButton("Fire1"))
                {
                    grabObject.SetGrabTransform(handmovement.GetLeftHandCurrentTransform());
                    HandManager.Instance.LeftHandGrabbedObject = grabObject;
                }
                if (Input.GetButton("Fire2"))
                {
                    grabObject.SetGrabTransform(handmovement.GetRightHandCurrentTransform());
                    HandManager.Instance.RightHandGrabbedObject = grabObject;
                }

            }
            else
            {
                //Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
                DisableLastOutline();
            }
        }
        else
        {
            //Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
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