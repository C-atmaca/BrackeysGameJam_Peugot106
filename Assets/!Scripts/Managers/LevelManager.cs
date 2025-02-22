using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<int> requiredAmountOfMoneyPerLevel;

    public void EndLevel()
    {
        MoneyManager.Instance.CalculateCostOfDamages();
    }
}