using DG.Tweening;
using System.Collections;
using UnityEngine;

public class HeatSolutionModule : SolutionSystem
{
    [SerializeField] private float moveAmount;
    [SerializeField] private float moveDuration;
    [SerializeField] private float animationStartDelay = 0.5f;

    private WaitForSeconds waitForSeconds;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Coroutine coroutine;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition - new Vector3(0, moveAmount, 0);

        waitForSeconds = new WaitForSeconds(animationStartDelay);
    }

    public override void StartExecuteSolution()
    {
        coroutine = StartCoroutine(MoveDown(endPosition));
    }

    public override void StopExecuteSolution()
    {
        StopCoroutine(coroutine);
        transform.DOKill();
        transform.DOMoveY(startPosition.y, moveDuration).SetEase(Ease.InSine);
        system.SetExecuteSolution(false);
        AudioManager.Instance.HeatReleaseStop();
    }

    private IEnumerator MoveDown(Vector3 targetPosition)
    {
        yield return waitForSeconds;
        transform.DOMoveY(targetPosition.y, moveDuration).SetEase(Ease.InSine);
        AudioManager.Instance.HeatReleasePlay();
        system.SetExecuteSolution(true);
    }
}