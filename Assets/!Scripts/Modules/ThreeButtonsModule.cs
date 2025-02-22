using UnityEngine;

public class ThreeButtonsModule : TriggerOnceSystem
{
    [SerializeField] private MeshRenderer lightBulb;

    private bool executeSolution = false;

    protected override void Start()
    {
        lightBulb.material.DisableKeyword("_EMISSION");

        base.Start();
    }

    protected override void Update()
    {
        if (executeSolution)
        {
            SetTriggered(false);
            return;
        }

        if (GetTriggered())
        {
            lightBulb.material.EnableKeyword("_EMISSION");
        }
        else
        {
            lightBulb.material.DisableKeyword("_EMISSION");
        }

        base.Update();
    }

    public override void SetExecuteSolution(bool value)
    {
        executeSolution = value;
    }
}
