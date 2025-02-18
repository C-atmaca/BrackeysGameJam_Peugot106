using DG.Tweening;
using UnityEngine;

public class HeatSolutionModule : SolutionSystem
{
    [SerializeField] private float moveAmount;
    [SerializeField] private float moveDuration;

    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition - new Vector3(0, moveAmount, 0);
    }

    public override void StartExecuteSolution()
    {
        transform.DOMoveY(endPosition.y, moveDuration).SetEase(Ease.InSine);
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        transform.DOMoveY(startPosition.y, moveDuration).SetEase(Ease.InSine);
        system.SetExecuteSolution(false);
    }
}