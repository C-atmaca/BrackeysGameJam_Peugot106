using UnityEngine;
using static HandType;

[RequireComponent(typeof(Outline))]
public abstract class SolutionSystem : MonoBehaviour
{
    [SerializeField] protected MechSystem system;
    public HandTypeEnum handType;
    public bool activateWithObject = false;
    public GrabObject grabObject;
    public Transform handPlacementTarget;
    public abstract void StartExecuteSolution();
    public abstract void StopExecuteSolution();
}