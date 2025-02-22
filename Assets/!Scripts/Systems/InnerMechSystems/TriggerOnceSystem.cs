using UnityEngine;

public class TriggerOnceSystem : MechSystem
{
    [SerializeField] protected TriggerOnceSystemSO data;

    private bool triggered = false;
    private int triggerTime = 0;
    private float timer = 0;
    private float damageStartTimer = 0f;
    private float damageTimer = 0f;

    protected virtual void Start()
    {
        triggerTime = SelectRandomTimeToTrigger();
    }

    protected virtual void Update()
    {
        if (LevelManager.Instance.GetPause()) return;

        if (triggered) 
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

            timer += Time.deltaTime * SystemsManager.Instance.GlobalData.GlobalMultiplier;
            if (timer > triggerTime)
            {
                triggered = true;
                triggerTime = SelectRandomTimeToTrigger();
                timer = 0;
            }
        }
    }

    public bool GetTriggered()
    {
        return triggered;
    }

    public void SetTriggered(bool value)
    {
        triggered = value;
    }

    public int SelectRandomTimeToTrigger()
    {
        return Random.Range(data.TriggerTimeMinInclusive, data.TriggerTimeMaxExclusive);
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