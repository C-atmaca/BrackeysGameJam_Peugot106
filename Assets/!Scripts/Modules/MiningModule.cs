using UnityEngine;
using static UnityEngine.UI.Image;
using UnityEngine.Animations.Rigging;

public class MiningModule : MechSystem
{
    [SerializeField] private Rock rock;
    [SerializeField] private MeshRenderer lightBulb;
    [SerializeField] private float rayDistance = 10;
    [SerializeField] private int damage = 10;
    [SerializeField] private int maxDamageMultiplier = 3;
    [SerializeField] private int minDamageMultiplier = -2;
    [SerializeField] private LineRenderer laserBeam;
    [SerializeField] private Transform laserBeamGameObject;

    private Transform mainCamTransform;
    private bool startMining;
    public bool isSongPlayed = false;
    private Vector3 endPoint;


    private void Start()
    {
        mainCamTransform = Camera.main.transform;
        laserBeamGameObject.gameObject.SetActive(false);
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

        if (!startMining) 
        {
            laserBeamGameObject.gameObject.SetActive(false);
            return;
        }

        if (Physics.SphereCast(rayOrigin, .10f, rayDirection, out RaycastHit hit, rayDistance))
        {
            var collider = hit.collider;
            if (collider.CompareTag("Rock"))
            {
                rock = collider.gameObject.GetComponent<Rock>();
                laserBeamGameObject.gameObject.SetActive(true);
                if (!isSongPlayed)
                {
                    AudioManager.Instance.MiningLaserPlay();
                    isSongPlayed = true;
                }
                endPoint = hit.point;
                laserBeamGameObject.LookAt(endPoint);
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
                    endPoint = hit.point;
                    laserBeamGameObject.LookAt(endPoint);
                }
                else if (collider.CompareTag("WeakMineSpot"))
                {
                    rock.DoDamage(damage * minDamageMultiplier);
                    rock.threeUpArrow.SetActive(false);
                    rock.oneUpArrow.SetActive(false);
                    rock.twoDownArrow.SetActive(true);
                    endPoint = hit.point;
                    laserBeamGameObject.LookAt(endPoint);
                }
                else
                {
                    rock.DoDamage(damage);
                    rock.threeUpArrow.SetActive(false);
                    rock.oneUpArrow.SetActive(true);
                    rock.twoDownArrow.SetActive(false);
                    endPoint = hit.point;
                    laserBeamGameObject.LookAt(endPoint);
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