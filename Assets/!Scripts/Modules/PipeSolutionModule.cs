using System.Collections;
using UnityEngine;

public class PipeSolutionModule : SolutionSystem
{
    private WaitForSeconds waitForSeconds;
    private Coroutine coroutine;

    public override void StartExecuteSolution()
    {
        coroutine = StartCoroutine(FixThePipe());
    }

    public override void StopExecuteSolution()
    {
        StopCoroutine(coroutine);
        system.SetExecuteSolution(false);
    } 

    private IEnumerator FixThePipe()
    {
        yield return waitForSeconds;
        system.SetExecuteSolution(true);
    }
}
