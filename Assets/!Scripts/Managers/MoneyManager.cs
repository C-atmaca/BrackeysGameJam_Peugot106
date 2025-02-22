using TMPro;
using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    [SerializeField] private int moneyPerRock = 10;
    [SerializeField] private int costPerDamage = 5;
    [SerializeField] private TextMeshProUGUI collectedRockAmountText;

    private int numberOfRocksCollectedThisRun = 0;

    private void Update()
    {
        collectedRockAmountText.text = numberOfRocksCollectedThisRun.ToString();
    }

    public void RockCollected()
    {
        numberOfRocksCollectedThisRun++;
    }

    public int GetRocksCollected()
    {
        return numberOfRocksCollectedThisRun;
    }

    public int CalculateMoneyEarned()
    {
        return numberOfRocksCollectedThisRun * moneyPerRock;
    }

    public int CalculateCostOfDamages()
    {
        return (MechHealthSystem.Instance.GetMaxHealth() - MechHealthSystem.Instance.GetCurrentHealth()) * costPerDamage;
    }
}