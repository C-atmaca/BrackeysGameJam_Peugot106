using UnityEngine;

public class HandManager : Singleton<HandManager>
{
    public SolutionSystem LeftHandSolutionSystem = null;
    public SolutionSystem RightHandSolutionSystem = null;
    public GrabObject LeftHandGrabbedObject;
    public GrabObject RightHandGrabbedObject;
    private void Update()
    {
        if (LeftHandSolutionSystem != null)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                LeftHandSolutionSystem.StopExecuteSolution();
                LeftHandSolutionSystem = null;    
            }
        }
        if (RightHandSolutionSystem != null)
        {
            if (Input.GetButtonUp("Fire2"))
            {
                RightHandSolutionSystem.StopExecuteSolution();

                RightHandSolutionSystem = null;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (LeftHandGrabbedObject != null)
            {
                LeftHandGrabbedObject.SetToOriginalPos();
                LeftHandGrabbedObject = null;
            }

        }
        if (Input.GetButtonUp("Fire2"))
        {
            if (RightHandGrabbedObject != null)
            {
                RightHandGrabbedObject.SetToOriginalPos();
                RightHandGrabbedObject = null;
            }

        }
    }
}