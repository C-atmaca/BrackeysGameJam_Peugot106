using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BoltModule : ConstantlyIncreasingSystem
{
    [SerializeField] private float countdownMinInclusive = 3;
    [SerializeField] private float countdownMaxExclusive = 6;
    [SerializeField] private float startRotation = 50f;
    [SerializeField] private float endRotation = -50f;
    [SerializeField] private GameObject bolt;

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
            bolt.transform.DOKill();
            bolt.transform.DORotate(Vector3.zero, 0f).SetEase(Ease.InOutQuart);
            return;
        }

        if (currentValue > 0)
        {
            bolt.transform.DORotate(new Vector3(0, 0, bolt.transform.rotation.z + 90), 0f).SetLoops(-1, LoopType.Incremental);
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
