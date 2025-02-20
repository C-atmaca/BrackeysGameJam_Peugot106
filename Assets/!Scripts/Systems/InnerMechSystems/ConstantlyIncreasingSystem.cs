using UnityEngine;

public class ConstantlyIncreasingSystem : MechSystem
{
    [SerializeField] protected ConstantlyIncreasingSystemSO data;
    [SerializeField] protected float currentValue;

    private float increaseRate;
    private float damageStartTimer = 0f;
    private float damageTimer = 0f;

    private void Start()
    {
        SetIncreaseRate(Random.Range(data.IncreaseRateMinInclusive, data.IncreaseRateMaxExclusive));
    }

    protected virtual void Update()
    {
        var increaseValue = increaseRate * SystemsManager.Instance.GlobalData.GlobalMultiplier * Time.deltaTime;
        ModifyCurrentValue(increaseValue);

        if (currentValue >= data.DamageThresholdMin && currentValue <= data.DamageThresholdMax)
        {
            damageStartTimer += Time.deltaTime;
            if (damageStartTimer >= data.DamageStartDelay)
            {
                DoDamage();
            } 
        }
        else
        {
            damageStartTimer = 0;
        }
    }

    protected void SetIncreaseRate(float value)
    {
        increaseRate = value;
    }

    public float GetCurrentValue()
    {
        return currentValue;
    }

    public void SetCurrentValue(float value)
    {
        currentValue = Mathf.Clamp01(value);
    }

    public void ModifyCurrentValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp01(currentValue);
    }

    protected virtual void DoDamage() 
    {
        damageTimer += Time.deltaTime;

        if (damageTimer >= data.DamageInterval)
        {
            MechHealthSystem.Instance.ModifyCurrentHealth(data.DamagePerInterval * -1);
            damageTimer = 0;
        }
    }
}