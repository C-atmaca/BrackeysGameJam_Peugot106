using System.Collections;
using UnityEngine;

public class FuelSolutionModule : SolutionSystem
{
    private WaitForSeconds waitForSeconds;
    private Coroutine coroutine;
    private bool isCoroutineStarted = false;
    private void Awake() 
    {
        activateWithObject = true;
    }

    private void Update() 
    {

    }

    public override void StartExecuteSolution()
    {
        if (HandManager.Instance.LeftHandSolutionSystem == this)
        {
            if (HandManager.Instance.LeftHandGrabbedObject != null)
            {
                if (HandManager.Instance.LeftHandGrabbedObject.GetComponent<GasTank>())
                {
                    coroutine = StartCoroutine(MoveUp());
                    isCoroutineStarted = true;
                }
                else
                {
                    return;
                }
            }


        }
        else if (HandManager.Instance.RightHandSolutionSystem == this)
        {
            if (HandManager.Instance.RightHandGrabbedObject != null)
            {
                if (HandManager.Instance.RightHandGrabbedObject.GetComponent<GasTank>())
                {
                    coroutine = StartCoroutine(MoveUp());
                    isCoroutineStarted = true;
                }
                else
                {
                    return;
                }
            }
        }
        
    }

    public override void StopExecuteSolution()
    {
        if (isCoroutineStarted)
        {
            StopCoroutine(coroutine);
            system.SetExecuteSolution(false);
        }
    }

    private IEnumerator MoveUp()
    {
        yield return waitForSeconds;
        system.SetExecuteSolution(true);
    }
}
