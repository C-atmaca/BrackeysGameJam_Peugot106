using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<int> requiredAmountOfMoneyPerLevel;
    [SerializeField] private Transform endOfLevelScreen;

    [SerializeField] private TextMeshProUGUI rocksCollectedAmountText;
    [SerializeField] private TextMeshProUGUI moneyEarnedAmountText;
    [SerializeField] private TextMeshProUGUI livingCostMoneyText;
    [SerializeField] private TextMeshProUGUI mechRepairCostMoneyText;
    [SerializeField] private TextMeshProUGUI totalIncomeMoneyText;
    [SerializeField] private TextMeshProUGUI dayText;

    private int TotalCost;
    private int CostOfDamages;
    private int CostOfDay;
    private int RocksCollected;
    private int MoneyEarned;
    private int TotalIncome;
    private int CurrentLevel = 0;

    private float levelTimer;
    [SerializeField] private float LevelLength;
    [SerializeField] private TextMeshProUGUI timer;

    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float endOfLevelScreenMoveAmount;
    [SerializeField] private float endOfLevelScreenMoveDuration;

    private bool levelEnded = false;

    private void Start()
    {
        startPosition = endOfLevelScreen.position;
        endPosition = startPosition - new Vector3(0f, endOfLevelScreenMoveAmount, 0f);

        StartLevel();
    }

    private void Update()
    {
        if (!levelEnded)
        {
            levelTimer -= Time.deltaTime;
        }

        if (levelTimer <= LevelLength)
        {
            levelEnded = true;

            if (levelEnded)
            {
                EndLevel();
            }
        }

        timer.text = ConvertSecondsToMinutes((int)levelTimer);
    }

    public void StartLevel()
    {
        levelTimer = LevelLength;
        levelEnded = false;

        endOfLevelScreen.DOLocalMoveY(startPosition.y, endOfLevelScreenMoveDuration).SetEase(Ease.InSine);
    }

    public void EndLevel()
    {
        CostOfDamages = MoneyManager.Instance.CalculateCostOfDamages();
        CostOfDay = requiredAmountOfMoneyPerLevel[CurrentLevel];
        TotalCost = CostOfDamages + CostOfDay;
        RocksCollected = MoneyManager.Instance.GetRocksCollected();
        MoneyEarned = MoneyManager.Instance.CalculateMoneyEarned();
        TotalIncome = MoneyEarned - TotalCost;

        rocksCollectedAmountText.text = RocksCollected.ToString();
        moneyEarnedAmountText.text = MoneyEarned.ToString();
        livingCostMoneyText.text = CostOfDay.ToString();
        mechRepairCostMoneyText.text = CostOfDamages.ToString();
        totalIncomeMoneyText.text = TotalIncome.ToString();
        dayText.text = "Day " + (CurrentLevel + 1);

        endOfLevelScreen.DOLocalMoveY(endPosition.y, endOfLevelScreenMoveDuration).SetEase(Ease.InSine);
        levelEnded = false;
    }

    public string ConvertSecondsToMinutes(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:00}:{seconds:00}";
    }
}