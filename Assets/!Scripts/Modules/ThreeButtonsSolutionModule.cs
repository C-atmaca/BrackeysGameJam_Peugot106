using DG.Tweening;
using UnityEngine;

public class ThreeButtonsSolutionModule : SolutionSystem
{
    [SerializeField] private float moveAmount;
    [SerializeField] private float moveDuration;
    [SerializeField] private float animationStartDelay = 0.5f;

    private WaitForSeconds waitForSeconds;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool previousState;

    private void Start()
    {
        startPosition = transform.localPosition;
        endPosition = startPosition - new Vector3(moveDuration / 4, moveAmount, moveDuration / 2);
        transform.DOMoveY(endPosition.y, moveDuration).SetEase(Ease.InSine);
        waitForSeconds = new WaitForSeconds(animationStartDelay);
    }

    private void Update()
    {
        bool currentState = ((ThreeButtonsModule)system).GetTriggered();
        if (currentState != previousState)
        {
            previousState = currentState;

            if (currentState)
            {
                transform.DOLocalMoveY(startPosition.y, moveDuration).SetEase(Ease.InSine);
            }
            else
            {
                transform.DOLocalMoveY(endPosition.y, moveDuration).SetEase(Ease.InSine);
            }
        }
    }

    public override void StartExecuteSolution()
    {
        system.SetExecuteSolution(true);
        AudioManager.Instance.ButtonVoice();
    }

    public override void StopExecuteSolution()
    {
        system.SetExecuteSolution(false);
    }
}