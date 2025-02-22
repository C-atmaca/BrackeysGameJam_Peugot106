using System.Collections;
using UnityEngine;

public class PipeSolutionModule : SolutionSystem
{
    private WaitForSeconds waitForSeconds;
    private Coroutine coroutine;
    private bool isCoroutineStarted = false;

    public override void StartExecuteSolution()
    {
        if (HandManager.Instance.LeftHandSolutionSystem == this)
        {
            if (HandManager.Instance.LeftHandGrabbedObject != null)
            {
                if (HandManager.Instance.LeftHandGrabbedObject.GetComponent<Hammer>())
                {
                    coroutine = StartCoroutine(FixThePipe());
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
                if (HandManager.Instance.RightHandGrabbedObject.GetComponent<Hammer>())
                {
                    coroutine = StartCoroutine(FixThePipe());
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

    private IEnumerator FixThePipe()
    {
        yield return waitForSeconds;
        system.SetExecuteSolution(true);
    }
}
