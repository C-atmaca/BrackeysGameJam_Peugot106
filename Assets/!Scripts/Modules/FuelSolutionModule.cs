using System.Collections;
using UnityEngine;

public class FuelSolutionModule : SolutionSystem
{
    private WaitForSeconds waitForSeconds;
    private Coroutine coroutine;


    public override void StartExecuteSolution()
    {
        coroutine = StartCoroutine(MoveUp());
    }

    public override void StopExecuteSolution()
    {
        StopCoroutine(coroutine);
        system.SetExecuteSolution(false);
    }

    private IEnumerator MoveUp()
    {
        yield return waitForSeconds;
        system.SetExecuteSolution(true);
    }
}
