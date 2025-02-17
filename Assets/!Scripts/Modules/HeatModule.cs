using System.Collections;
using UnityEngine;

public class HeatModule : ConstantlyIncreasingSystem
{
    [SerializeField] private float countdown = 2;
    [SerializeField] private float startRotation = 50f;
    [SerializeField] private float endRotation = -50f;

    private WaitForSeconds waitForSeconds;
    private bool countdownHasStarted = false;

    [SerializeField] private Transform heatNeedleTransform;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(countdown);
    }

    protected override void Update()
    {
        if (Input.GetButton("Fire2"))
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

            StartCoroutine(Countdown());
            countdownHasStarted = true;
        }
    }

    private void LateUpdate()
    {
        float rotation = Mathf.Lerp(startRotation, endRotation, currentValue);
        heatNeedleTransform.localRotation = Quaternion.Euler(0, rotation, 0);
    }

    private IEnumerator Countdown()
    {
        while (currentValue <= 0)
        {
            yield return waitForSeconds;

            currentValue += Mathf.Epsilon;
            countdownHasStarted = false;

            yield break;
        }
    }
}