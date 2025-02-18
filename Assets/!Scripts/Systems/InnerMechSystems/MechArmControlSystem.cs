using DG.Tweening;
using UnityEngine;

public enum MechArmType
{
    None, Drill, Vacuum
};

public class MechArmControlSystem : MonoBehaviour
{
    [SerializeField] private Transform drillTransform;
    [SerializeField] private Transform vacuumTransform;
    [SerializeField] private Camera mechCam;
    private Vector3  drillOriginalPos;
    private Vector3  vacuumOriginalPos;
    private Transform targetTransform;
    private MechArmType mechArmType = MechArmType.Drill;
    [SerializeField] KeyCode switchArmType;

    private LayerMask layerMask;

    private bool isDrillInitalPos = true;

    private void Awake()
    {
        layerMask = LayerMask.GetMask("Mine");
        drillOriginalPos = drillTransform.position;
        vacuumOriginalPos = vacuumTransform.position;    
    }

    private void Update() 
    {
        if (Input.GetKeyDown(switchArmType))
        {
            SwitchArm();
        }

        Ray ray = mechCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask)) 
        {
            ArmDecision(hit.point);
        }
        else
        {
            DrillCancel();
            VacuumCancel();
        }
    }

    private void ArmDecision(Vector3 target)
    {
        switch (mechArmType)
        {
            case MechArmType.Drill:
                Drill(target);
            break;
            case MechArmType.Vacuum:
                Vacuum(target);
            break;
            case MechArmType.None:
                
            break;
        }
    }

    private void Drill(Vector3 target)
    {
        drillTransform.DOMove(target, 2f);
    }

    private void DrillCancel()
    {
        drillTransform.DOMove(drillOriginalPos, 1f);
    }

    private void Vacuum(Vector3 target)
    {
        vacuumTransform.DOMove(target, 2f);
    }

    private void VacuumCancel()
    {
        vacuumTransform.DOMove(vacuumOriginalPos, 1f);
    }

    private void SwitchArm()
{
    if (mechArmType == MechArmType.Drill)
    {
        DrillCancel();
        mechArmType = MechArmType.Vacuum;
    }
    else
    {
        VacuumCancel();
        mechArmType = MechArmType.Drill;
    }
}
}
