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
        startPosition = new Vector3(transform.localPosition.x, -0.1602f, transform.localPosition.z);
        endPosition = new Vector3(transform.localPosition.x, -0.17389f, transform.localPosition.z);
    }

    private void Update()
    {
        bool currentState = ((ThreeButtonsModule)system).GetTriggered();
        if (currentState != previousState)
        {
            previousState = currentState;

            if (currentState)
            {
                transform.localPosition = startPosition;
            }
            else
            {
                transform.localPosition = endPosition;
            }
        }
    }

    public override void StartExecuteSolution()
    {
        system.SetExecuteSolution(true);
        AudioManager.Instance.ButtonVoicePlay();
    }

    public override void StopExecuteSolution()
    {
        system.SetExecuteSolution(false);
        AudioManager.Instance.isLightPlayed = false;
        AudioManager.Instance.ButtonVoiceStop();
        AudioManager.Instance.LightBulbSoundStop();
    }
}