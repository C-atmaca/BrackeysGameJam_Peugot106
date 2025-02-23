using System.Collections;
using UnityEngine;

public class HeatModule : ConstantlyIncreasingSystem
{
    [SerializeField] private float countdownMinIclusive = 3;
    [SerializeField] private float countdownMaxExclusive = 6;
    [SerializeField] private float startRotation = 50f;
    [SerializeField] private float endRotation = -50f;

    private WaitForSeconds waitForSeconds;
    private float twitchAmount = 0;
    private bool countdownHasStarted = false;
    private bool executeSolution = false;

    [SerializeField] private Transform heatNeedleTransform1;
    [SerializeField] private Transform heatNeedleTransform2;
    [SerializeField] private Transform heatNeedleTransform3;

    private bool isSoundPlayed = false;

    protected override void Update()
    {
        if (executeSolution)
        {
            ModifyCurrentValue((data.DecreaseRate * Time.deltaTime) * -1);
            return;
        }

        if (currentValue > 0)
        {
            if (!isSoundPlayed)
            {
                AudioManager.Instance.BreathingPlay();
                isSoundPlayed = true;
            }
            base.Update();
        }
        else
        {
            if (countdownHasStarted) return;
            waitForSeconds = new WaitForSeconds(Random.Range((int)countdownMinIclusive, (int)countdownMaxExclusive));

            StartCoroutine(Countdown());
            countdownHasStarted = true;
        }
        if (currentValue == 0)
        {
            AudioManager.Instance.BreathingStop();
            isSoundPlayed = false;
        }
    }

    public override void SetExecuteSolution(bool value)
    {
        executeSolution = value;
    }

    public bool GetExecuteSolution()
    {
        return executeSolution;
    }

    private void LateUpdate()
    {
        if (currentValue > data.DamageThresholdMin)
        {
            twitchAmount = Mathf.Sin(Time.time * 70f) * Random.Range(-2f, 2f);
            GeneralIndicatorSystem.Instance.ToggleHeatErrorIndicator(true);
        }
        else
        {
            GeneralIndicatorSystem.Instance.ToggleHeatErrorIndicator(false);
        }

        float rotation = Mathf.Lerp(startRotation, endRotation, currentValue) + twitchAmount;
        heatNeedleTransform1.localRotation = Quaternion.Euler(0, rotation, 0);
        heatNeedleTransform2.localRotation = Quaternion.Euler(0, rotation, 0);
        heatNeedleTransform3.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    private IEnumerator Countdown()
    {
        while (currentValue <= 0)
        {
            yield return waitForSeconds;

            currentValue += Mathf.Epsilon;
            countdownHasStarted = false;

            SetIncreaseRate(Random.Range(data.IncreaseRateMinInclusive, data.IncreaseRateMaxExclusive));
            yield break;
        }
    }
}