using System.Collections;
using UnityEngine;

public class WaterPipeModule : ConstantlyIncreasingSystem
{
    [SerializeField] private float countdownMinInclusive = 3;
    [SerializeField] private float countdownMaxExclusive = 6;
    [SerializeField] GameObject particleObject;
    private bool executeSolution = false;
    private WaitForSeconds waitForSeconds;
    private bool countdownHasStarted = false;
    private void Start() 
    {
        SetIncreaseRate(Random.Range(data.IncreaseRateMinInclusive, data.IncreaseRateMaxExclusive));
    }

    protected override void Update()
    {
        if (executeSolution)
        {
            ModifyCurrentValue((data.DecreaseRate * Time.deltaTime) * -1);
            particleObject.SetActive(false);
            return;
        }

        if (currentValue > 0)
        {
            particleObject.SetActive(true);
            base.Update();
        }

        else
        {
            if (countdownHasStarted) return;
            waitForSeconds = new WaitForSeconds(Random.Range((int)countdownMinInclusive, (int)countdownMaxExclusive));

            StartCoroutine(Countdown());
            countdownHasStarted = true;
        }
    }

    private void LateUpdate() 
    {
        currentValue = GetCurrentValue();
    }

    public override void SetExecuteSolution(bool value)
    {
        executeSolution = value;
    }

    public bool GetExecuteSolution()
    {
        return executeSolution;
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
