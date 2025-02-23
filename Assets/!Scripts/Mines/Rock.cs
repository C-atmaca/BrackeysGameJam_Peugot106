using System.Collections;
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

    private bool isSoundPlayed = false;
    private ParticleSystem particleSystem;

    private void Awake() 
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();  
    }

    private void Start()
    {
        currentProgress = 0;
        
        threeUpArrow.SetActive(false);
        oneUpArrow.SetActive(false);
        twoDownArrow.SetActive(false);

        sweetSpotMin = Random.Range(20, 71);
        sweetSpotMax = sweetSpotMin + 10;
        breakThreshold = sweetSpotMax + 1;
    }

    private void Update()
    {
        UpdateProgressBar();

        if (!doingDamage) return;

        if (currentProgress >= maxProgress)
        {
            if (!isSoundPlayed)
            {
                AudioManager.Instance.RockFailedPlay();
                particleSystem.Play();
                isSoundPlayed = true;
            }
            StartCoroutine(DestroyLatency(0.9f, gameObject));
        }

        if (currentProgress >= breakThreshold)
        {
            breakTimer += Time.deltaTime;

            if (breakTimer > breakInterval)
            {
                if (!isSoundPlayed)
                {
                    AudioManager.Instance.RockFailedPlay();
                    particleSystem.Play();
                    isSoundPlayed = true;
                }
                StartCoroutine(DestroyLatency(0.9f, gameObject));
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
                if (!isSoundPlayed)
                {
                    AudioManager.Instance.RockSuccesfullPlay();
                    particleSystem.Play();
                    MoneyManager.Instance.RockCollected();
                    isSoundPlayed = true;
                }

                StartCoroutine(DestroyLatency(0.9f, gameObject));
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

    private IEnumerator DestroyLatency(float time, GameObject gameObject)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}