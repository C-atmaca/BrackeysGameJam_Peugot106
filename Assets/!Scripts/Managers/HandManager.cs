using UnityEngine;

public class HandManager : Singleton<HandManager>
{
    public SolutionSystem LeftHandSolutionSystem = null;
    public SolutionSystem RightHandSolutionSystem = null;

    private void Update()
    {
        if (LeftHandSolutionSystem != null)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                Debug.Log("Left Hand Has Stopped Being Used!");

                LeftHandSolutionSystem.StopExecuteSolution();
                LeftHandSolutionSystem = null;
            }
        }
        if (RightHandSolutionSystem != null)
        {
            if (Input.GetButtonUp("Fire2"))
            {
                Debug.Log("Right Hand Has Stopped Being Used!");

                RightHandSolutionSystem.StopExecuteSolution();
                RightHandSolutionSystem = null;
            }
        }
    }
}