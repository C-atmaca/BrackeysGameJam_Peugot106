using UnityEngine;
using UnityEngine.UI;

public class Rock : MonoBehaviour
{
    private int maxProgress = 100;
    [SerializeField] private int currentProgress;
    [SerializeField] private int breakThreshold = 70;
    [SerializeField] private int sweetSpotMin = 50;
    [SerializeField] private int sweetSpotMax = 40;
    [SerializeField] private Image progressBar;
    [SerializeField] private float fillSpeed = 5f;
    public GameObject oneUpArrow;
    public GameObject threeUpArrow;
    public GameObject twoDownArrow;

    private float targetFillAmount;

    private float damageTimer = 0;
    [SerializeField] private float damageInterval = 2;
    private float successTimer = 0;
    [SerializeField] private float successInterval = 3;
    private float breakTimer = 0;
    [SerializeField] private float breakInterval = 2;

    public bool doingDamage = false;

    private void Start()
    {
        currentProgress = 0;
        
        threeUpArrow.SetActive(false);
        oneUpArrow.SetActive(false);
        twoDownArrow.SetActive(false);
    }

    private void Update()
    {
        UpdateProgressBar();

        if (!doingDamage) return;

        if (currentProgress >= maxProgress)
        {
            Debug.Log("ROCK GOT BROKEN!");
            Destroy(gameObject);
        }

        if (currentProgress >= breakThreshold)
        {
            Debug.Log("in breakthreshold");
            breakTimer += Time.deltaTime;

            if (breakTimer > breakInterval)
            {
                Debug.Log("ROCK GOT BROKEN!");
                Destroy(gameObject);
            }
        }
        else
        {
            breakTimer = 0;
        }

        if (currentProgress <= sweetSpotMax && currentProgress >= sweetSpotMin)
        {
            successTimer += Time.deltaTime;

            if (successTimer > successInterval)
            {
                MoneyManager.Instance.RockCollected();
                Destroy(gameObject);
            }
        }
        else 
        {
            successTimer = 0;
        }
    }

    public void DoDamage(int damage)
    {
        damageTimer += Time.deltaTime;

        if (damageTimer >= damageInterval)
        {
            currentProgress += damage;
            damageTimer = 0;
        }

        if (currentProgress <= 0)
        {
            currentProgress = 0;
        }
    }

    private void UpdateProgressBar()
    {
        targetFillAmount = (float)currentProgress / (float)maxProgress;

        progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);

        Color targetColor;
        if (currentProgress >= breakThreshold)
        {
            targetColor = Color.red;
        }
        else if (currentProgress >= sweetSpotMin && currentProgress <= sweetSpotMax)
        {
            targetColor = Color.green;
        }
        else
        {
            targetColor = Color.yellow;
        }

        progressBar.color = Color.Lerp(progressBar.color, targetColor, Time.deltaTime * fillSpeed);
    }
}