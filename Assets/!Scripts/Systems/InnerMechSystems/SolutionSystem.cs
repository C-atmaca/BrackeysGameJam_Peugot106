using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class SolutionSystem : MonoBehaviour
{
    [SerializeField] protected MechSystem system;

    public abstract void StartExecuteSolution();
    public abstract void StopExecuteSolution();
}