using UnityEngine;

public class ValveSolutionModule : SolutionSystem
{
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float distanceFromCamera = 10f;

    private Camera mainCamera;
    private bool startedSolution = false;

    private bool firstCheckpoint = false;
    private bool secondCheckpoint = false;
    private bool thirdCheckpoint = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!startedSolution) return;

        RotateToMouse();
        DecreaseValueBasedOnRotation();
    }

    public override void StartExecuteSolution()
    {
        startedSolution = true;
    }

    void RotateToMouse()
    {
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera));

        Vector3 direction = (mouseWorldPos - transform.position).normalized;

        float angle = Vector3.SignedAngle(transform.forward, direction, transform.right);

        transform.Rotate(Vector3.right, angle * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void DecreaseValueBasedOnRotation()
    {
        float rotation = transform.eulerAngles.x;
        if (rotation >= 280 && rotation <= 300)
        {
            firstCheckpoint = true;
        }
        else if (rotation >= 0 && rotation <= 20)
        {
            secondCheckpoint = true;
        }
        else if (rotation >= 50 && rotation <= 70)
        {
            thirdCheckpoint = true;
        }

        if (firstCheckpoint && secondCheckpoint && thirdCheckpoint) 
        {
            ((ValveModule)system).DecreaseValueWithRotation();
            firstCheckpoint = false;
            secondCheckpoint = false;
            thirdCheckpoint = false;
        }
    }

    public override void StopExecuteSolution()
    {
        startedSolution = false;
    }
}