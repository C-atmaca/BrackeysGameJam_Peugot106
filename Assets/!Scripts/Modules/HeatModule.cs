using System.Collections;
using UnityEngine;

public class HeatModule : ConstantlyIncreasingSystem
{
    [SerializeField] private float countdownMinIclusive = 3;
    [SerializeField] private float countdownMaxExclusive = 6;
    [SerializeField] private float startRotation = 50f;
    [SerializeField] private float endRotation = -50f;
    [SerializeField] private float twitchAmount = 0;

    private WaitForSeconds waitForSeconds;
    private bool countdownHasStarted = false;
    private bool executeSolution = false;

    [SerializeField] private Transform heatNeedleTransform;

    protected override void Update()
    {
        if (executeSolution)
        {
            ModifyCurrentValue((data.DecreaseRate * Time.deltaTime) * -1);
            return;
        }

        if (currentValue > 0)
        {
            base.Update();
        }
        else
        {
            if (countdownHasStarted) return;
            waitForSeconds = new WaitForSeconds(Random.Range((int)countdownMinIclusive, (int)countdownMaxExclusive));

            StartCoroutine(Countdown());
            countdownHasStarted = true;
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
            twitchAmount = Mathf.Sin(Time.time * 50f) * Random.Range(-2f, 2f);
        }

        float rotation = Mathf.Lerp(startRotation, endRotation, currentValue) + twitchAmount;
        heatNeedleTransform.localRotation = Quaternion.Euler(0, rotation, 0);
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