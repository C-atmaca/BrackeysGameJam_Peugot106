using UnityEngine;

public class SpawnInsideMesh : MonoBehaviour
{
    public GameObject objectToSpawn;
    public MeshFilter targetMeshFilter;
    public int spawnCount = 5;

    void Start()
    {
        if (targetMeshFilter == null || objectToSpawn == null)
        {
            Debug.LogError("MeshFilter is missing.");
            return;
        }

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = GetRandomPointInMesh(targetMeshFilter.mesh);
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPointInMesh(Mesh mesh)
    {
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;

        int triangleIndex = Random.Range(0, triangles.Length / 3) * 3;

        Vector3 v0 = vertices[triangles[triangleIndex]];
        Vector3 v1 = vertices[triangles[triangleIndex + 1]];
        Vector3 v2 = vertices[triangles[triangleIndex + 2]];

        Transform meshTransform = targetMeshFilter.transform;
        v0 = meshTransform.TransformPoint(v0);
        v1 = meshTransform.TransformPoint(v1);
        v2 = meshTransform.TransformPoint(v2);

        return GetRandomPointInTriangle(v0, v1, v2);
    }

    Vector3 GetRandomPointInTriangle(Vector3 a, Vector3 b, Vector3 c)
    {
        float r1 = Mathf.Sqrt(Random.value);
        float r2 = Random.value;

        return (1 - r1) * a + (r1 * (1 - r2)) * b + (r1 * r2) * c;
    }
}