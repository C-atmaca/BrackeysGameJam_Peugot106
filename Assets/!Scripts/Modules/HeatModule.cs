using System.Collections;
using UnityEngine;

public class HeatModule : ConstantlyIncreasingSystem
{
    [SerializeField] private float countdown = 2;

    private WaitForSeconds waitForSeconds;

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
        if (currentValue <= 0)
        {
            StartCoroutine(Countdown());
        }
        base.Update();
    }

    private IEnumerator Countdown()
    {
        while (currentValue <= 0)
        {
            yield return waitForSeconds;
        }
    }
}