using TMPro;
using UnityEngine;

public class MechHealthSystem : Singleton<MechHealthSystem>
{
    [SerializeField] private HealthSO data;
    [SerializeField] private int currentHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject gameOverCanvas;

    private void Start()
    {
        currentHealth = data.MaxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            // play death audio and vfx then you can maybe wait a sec for the vfx to finish or something
            gameOverCanvas.SetActive(true);
            LevelManager.Instance.SetPause(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void SetCurrentHealth(int value)
    {
        currentHealth = value;
        SetHealthText();
    }

    public void ModifyCurrentHealth(int value)
    {
        if (currentHealth <= 0) return;
        currentHealth += value;
        SetHealthText();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return data.MaxHealth;
    }

    private void SetHealthText()
    {
        healthText.text = currentHealth.ToString() + '%';
    }
}