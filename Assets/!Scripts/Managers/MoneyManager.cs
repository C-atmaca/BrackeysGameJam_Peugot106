using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>
{
    [SerializeField] private int moneyPerRock = 10;

    private int numberOfRocksCollectedThisRun;

    public void RockCollected()
    {
        numberOfRocksCollectedThisRun++;
    }

    public int CalculateReward()
    {
        return numberOfRocksCollectedThisRun * moneyPerRock;
    }

    public int CalculateCostOfDamages()
    {
        return 0;
    }
}