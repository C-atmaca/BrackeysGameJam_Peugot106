using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class HeatSolutionModule : SolutionSystem
{
    [SerializeField] private float moveAmount;
    [SerializeField] private float moveDuration;
    [SerializeField] private float animationStartDelay = 0.5f;

    private WaitForSeconds waitForSeconds;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition - new Vector3(0, moveAmount, 0);

        waitForSeconds = new WaitForSeconds(animationStartDelay);
    }

    public override void StartExecuteSolution()
    {
        StartCoroutine(MoveDown(endPosition));
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        transform.DOMoveY(startPosition.y, moveDuration).SetEase(Ease.InSine);
        system.SetExecuteSolution(false);
    }

    private IEnumerator MoveDown(Vector3 targetPosition)
    {
        yield return waitForSeconds;
        transform.DOMoveY(targetPosition.y, moveDuration).SetEase(Ease.InSine);
    }
}