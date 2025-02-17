using UnityEngine;
using static UnityEngine.UI.Image;

public class RaycastFromCamera : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private float rayDistance;

    private void FixedUpdate()
    {
        Vector3 rayOrigin = targetObject.position;
        Vector3 rayDirection = targetObject.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, rayDistance))
        {
            if (hit.collider.CompareTag("ActionObject"))
            {
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.green);

                hit.collider.TryGetComponent<SolutionSystem>(out SolutionSystem solutionSystem);

                if (Input.GetButton("Fire1"))
                {
                    if (HandManager.Instance.LeftHandSolutionSystem != null) return;

                    HandManager.Instance.LeftHandSolutionSystem = solutionSystem;
                    solutionSystem.StartExecuteSolution();
                }
                else if (Input.GetButton("Fire2"))
                {
                    if (HandManager.Instance.RightHandSolutionSystem != null) return;

                    HandManager.Instance.RightHandSolutionSystem = solutionSystem;
                    solutionSystem.StartExecuteSolution();
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);
        }
    }
}