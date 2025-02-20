using UnityEngine;

public class ThreeButtonsModule : TriggerOnceSystem
{
    [SerializeField] private GameObject lightObject;

    private bool executeSolution = false;

    protected override void Start()
    {
        lightObject.SetActive(false);

        base.Start();
    }

    protected override void Update()
    {
        if (executeSolution)
        {
            SetTriggered(false);
            return;
        }

        lightObject.SetActive(GetTriggered());

        base.Update();
    }

    public override void SetExecuteSolution(bool value)
    {
        executeSolution = value;
    }
}
