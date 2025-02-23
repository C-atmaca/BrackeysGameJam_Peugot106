using UnityEngine;

public class GeneralIndicatorSystem : Singleton<GeneralIndicatorSystem>
{
    [SerializeField] private GameObject ValveErrorIndicator;
    [SerializeField] private GameObject FuelErrorIndicator;
    [SerializeField] private GameObject HeatErrorIndicator;

    private void Start()
    {
        ValveErrorIndicator.SetActive(false);
        FuelErrorIndicator.SetActive(false);
    }

    public void ToggleValveErrorIndicator(bool value)
    {
        ValveErrorIndicator.SetActive(value);
    }

    public void ToggleFuelErrorIndicator(bool value)
    {
        FuelErrorIndicator.SetActive(value);
    }

    public void ToggleHeatErrorIndicator(bool value)
    {
        HeatErrorIndicator.SetActive(value);
    }
}