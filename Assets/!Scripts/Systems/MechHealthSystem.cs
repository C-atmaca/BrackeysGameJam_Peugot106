using TMPro;
using UnityEngine;

public class MechHealthSystem : Singleton<MechHealthSystem>
{
    [SerializeField] private HealthSO data;
    [SerializeField] private int currentHealth;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        currentHealth = data.MaxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("MECH EXPLODED!");
        }
    }

    public void SetCurrentHealth(int value)
    {
        currentHealth = value;
        SetHealthText();
    }

    public void ModifyCurrentHealth(int value)
    {
        currentHealth += value;
        SetHealthText();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void SetHealthText()
    {
        healthText.text = currentHealth.ToString() + '%';
    }
}