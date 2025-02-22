using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<int> requiredAmountOfMoneyPerLevel;
    [SerializeField] private Transform endOfLevelScreen;

    [SerializeField] private TextMeshProUGUI rocksCollectedAmountText;
    [SerializeField] private TextMeshProUGUI moneyEarnedAmountText;
    [SerializeField] private TextMeshProUGUI livingCostMoneyText;
    [SerializeField] private TextMeshProUGUI mechRepairCostMoneyText;
    [SerializeField] private TextMeshProUGUI totalIncomeMoneyText;
    [SerializeField] private TextMeshProUGUI previousDayIncomeMoneyText;
    [SerializeField] private TextMeshProUGUI dayText;

    [SerializeField] private GameObject cantPayButton;
    [SerializeField] private GameObject debtText;

    private int totalCost;
    private int costOfDamages;
    private int costOfDay;
    private int rocksCollected;
    private int moneyEarned;
    private int totalIncome;
    private int currentLevel = 0;
    private int previousDayIncome = 0;

    private float levelTimer;
    [SerializeField] private List<float> LevelLength;
    [SerializeField] private TextMeshProUGUI timer;

    private Vector3 startPosition;
    private Vector3 endPosition;
    [SerializeField] private float endOfLevelScreenMoveAmount;
    [SerializeField] private float endOfLevelScreenMoveDuration;

    private bool levelEnded = false;
    private bool pause = false;

    private void Start()
    {
        startPosition = endOfLevelScreen.position;
        endPosition = startPosition - new Vector3(0f, endOfLevelScreenMoveAmount, 0f);

        StartLevel();
    }

    private void Update()
    {
        if (pause) return;

        if (!levelEnded)
        {
            levelTimer -= Time.deltaTime;
        }

        if (levelTimer <= 0)
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
        currentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);

        if (currentLevel <= LevelLength.Count)
        {
            levelTimer = LevelLength[currentLevel];
        }
        else
        {
            levelTimer = LevelLength[0];
        }

        levelEnded = false;

        endOfLevelScreen.DOMoveY(startPosition.y, endOfLevelScreenMoveDuration).SetEase(Ease.InSine);
    }

    public void EndLevel()
    {
        costOfDamages = MoneyManager.Instance.CalculateCostOfDamages();
        costOfDay = requiredAmountOfMoneyPerLevel[currentLevel];
        totalCost = costOfDamages + costOfDay;
        rocksCollected = MoneyManager.Instance.GetRocksCollected();
        moneyEarned = MoneyManager.Instance.CalculateMoneyEarned();
        previousDayIncome = PlayerPrefs.GetInt("PreviousDayIncome", 0);
        totalIncome = moneyEarned - totalCost + previousDayIncome;

        rocksCollectedAmountText.text = rocksCollected.ToString();
        moneyEarnedAmountText.text = "+" + moneyEarned;
        livingCostMoneyText.text = "-" + costOfDay;
        mechRepairCostMoneyText.text = "-" + costOfDamages;
        totalIncomeMoneyText.text = totalIncome.ToString();
        previousDayIncomeMoneyText.text = previousDayIncome.ToString();
        dayText.text = "Day " + (currentLevel + 1);

        if (totalIncome < 0)
        {
            cantPayButton.SetActive(true);
            debtText.SetActive(true);
        }
        else
        {
            cantPayButton.SetActive(false);
            debtText.SetActive(false);
        }

        endOfLevelScreen.DOMoveY(endPosition.y, endOfLevelScreenMoveDuration).SetEase(Ease.InSine);
        levelEnded = false;
        SetPause(true);
    }

    public void TryAgain()
    {
        SetPause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        previousDayIncome = totalIncome;
        PlayerPrefs.SetInt("PreviousDayIncome", previousDayIncome);
        currentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        SetPause(false);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more scenes to load.");
        }
    }

    public void SetPause(bool isPause)
    {
        pause = isPause;
    }

    public bool GetPause()
    {
        return pause;
    }

    public string ConvertSecondsToMinutes(int totalSeconds)
    {
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return $"{minutes:00}:{seconds:00}";
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}