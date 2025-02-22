using DG.Tweening;
using UnityEngine;

public class ThreeButtonsSolutionModule : SolutionSystem
{
    [SerializeField] private float moveAmount;
    [SerializeField] private float moveDuration;
    [SerializeField] private float animationStartDelay = 0.5f;

    private Vector3 startPosition;
    private Vector3 startPositionLocal;
    private Vector3 endPosition;
    private bool previousState;

    private void Start()
    {
        startPosition = new Vector3(); ;
        startPositionLocal = transform.localPosition;
        endPosition = startPositionLocal - new Vector3(0f, moveAmount, 0f);
        transform.position = endPosition;
        Debug.Log("start pos" + startPosition);
        Debug.Log("start pos local" + startPositionLocal);
        Debug.Log("end pos" + endPosition);
    }

    private void Update()
    {
        bool currentState = ((ThreeButtonsModule)system).GetTriggered();
        if (currentState != previousState)
        {
            previousState = currentState;

            if (currentState)
            {
                Debug.Log("start pos");
                transform.localPosition = startPositionLocal;
            }
            else
            {
                Debug.Log("end pos");
                transform.localPosition = endPosition;
            }
        }
    }

    public override void StartExecuteSolution()
    {
        system.SetExecuteSolution(true);
    }

    public override void StopExecuteSolution()
    {
        system.SetExecuteSolution(false);
    }
}