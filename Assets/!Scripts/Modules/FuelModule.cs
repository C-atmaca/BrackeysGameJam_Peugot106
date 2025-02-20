using System.Collections;
using UnityEngine;

public class FuelModule : ConstantlyIncreasingSystem
{
    [SerializeField] private float countdownMinInclusive = 3;
    [SerializeField] private float countdownMaxExclusive = 6;
    [SerializeField] private float startRotation = 50f;
    [SerializeField] private float endRotation = -50f;
    [SerializeField] private GameObject[] fuelBars;

    private WaitForSeconds waitForSeconds;
    private bool countdownHasStarted = false;
    private bool executeSolution = false;

    private void Start() 
    {
        SetIncreaseRate(Random.Range(data.IncreaseRateMinInclusive, data.IncreaseRateMaxExclusive));
    }

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
            waitForSeconds = new WaitForSeconds(Random.Range((int)countdownMinInclusive, (int)countdownMaxExclusive));

            StartCoroutine(Countdown());
            countdownHasStarted = true;
        }
    }

    private void LateUpdate() 
    {
        currentValue = GetCurrentValue();
        DeactivateFuelBars();
        ActivateFuelBars();
    }

    private void DeactivateFuelBars()
    {
        if (currentValue >= .25f)
        {
            fuelBars[0].SetActive(false);
        }
        if (currentValue >= .50f)
        {
            fuelBars[1].SetActive(false);
        }
        if (currentValue >= .75f)
        {
            fuelBars[2].SetActive(false);
        }
        if (currentValue >= 1f)
        {
            fuelBars[3].SetActive(false);
        }
    }

    private void ActivateFuelBars()
    {
        if (currentValue <= .25f)
        {
            fuelBars[0].SetActive(true);
        }
        if (currentValue <= .50f)
        {
            fuelBars[1].SetActive(true);
        }
        if (currentValue <= .75f)
        {
            fuelBars[2].SetActive(true);
        }
        if (currentValue <= .99f)
        {
            fuelBars[3].SetActive(true);
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
