using UnityEngine;
using UnityEngine.UIElements;

public class MiningModule : MechSystem
{
    [SerializeField] private Rock rock;
    [SerializeField] private MeshRenderer lightBulb;
    [SerializeField] private float rayDistance = 10;
    [SerializeField] private int damage = 10;
    [SerializeField] private int maxDamageMultiplier = 3;
    [SerializeField] private int minDamageMultiplier = -2;

    private Transform mainCamTransform;
    private bool startMining;

    private void Start()
    {
        mainCamTransform = Camera.main.transform;
    }

    private void Update()
    {
        if (rock != null)
        {
            rock.doingDamage = startMining;
        }

        Vector3 rayOrigin = mainCamTransform.position;
        Vector3 rayDirection = mainCamTransform.forward;

        if (Physics.SphereCast(rayOrigin, .10f, rayDirection, out RaycastHit hitt, rayDistance))
        {
            var collider = hitt.collider;
            if (collider.CompareTag("StrongMineSpot") || collider.CompareTag("WeakMineSpot") || collider.CompareTag("Rock"))
            {
                lightBulb.material.EnableKeyword("_EMISSION");
            }
            else
            {
                lightBulb.material.DisableKeyword("_EMISSION");
            }
        }
        else
        {
            lightBulb.material.DisableKeyword("_EMISSION");
        }

        if (!startMining) return;

        if (Physics.SphereCast(rayOrigin, .10f, rayDirection, out RaycastHit hit, rayDistance))
        {
            var collider = hit.collider;
            if (collider.CompareTag("Rock"))
            {
                rock = collider.gameObject.GetComponent<Rock>();
                lightBulb.material.EnableKeyword("_EMISSION");
            }
            if (collider.CompareTag("StrongMineSpot") || collider.CompareTag("WeakMineSpot") || collider.CompareTag("Rock"))
            {
                lightBulb.material.EnableKeyword("_EMISSION");
            }
            else
            {
                lightBulb.material.DisableKeyword("_EMISSION");
            }

            if (rock != null)
            {
                if (collider.CompareTag("StrongMineSpot"))
                {
                    rock.DoDamage(damage * maxDamageMultiplier);
                    rock.threeUpArrow.SetActive(true);
                    rock.oneUpArrow.SetActive(false);
                    rock.twoDownArrow.SetActive(false);
                }
                else if (collider.CompareTag("WeakMineSpot"))
                {
                    rock.DoDamage(damage * minDamageMultiplier);
                    rock.threeUpArrow.SetActive(false);
                    rock.oneUpArrow.SetActive(false);
                    rock.twoDownArrow.SetActive(true);
                }
                else
                {
                    rock.DoDamage(damage);
                    rock.threeUpArrow.SetActive(false);
                    rock.oneUpArrow.SetActive(true);
                    rock.twoDownArrow.SetActive(false);
                }
            }
        }
    }

    public override void SetExecuteSolution(bool value)
    {
        startMining = value;
    }

    //void OnDrawGizmos()
    //{
    //    Vector3 origin = mainCamTransform.position;
    //    Vector3 direction = mainCamTransform.forward;
    //    float radius = .10f;
    //    float maxDistance = rayDistance;

    //    int segments = 50;
    //    for (int i = 0; i <= segments; i++)
    //    {
    //        float t = (i / (float)segments);
    //        Vector3 point = origin + direction * (t * maxDistance);
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawWireSphere(point, radius);
    //    }
    //}
}