using System.Collections.Generic;
using UnityEngine;

public class SpawnInsideMesh : MonoBehaviour
{
    public GameObject strongMineSpot;
    public GameObject weakMineSpot;
    public int strongSpawnCount = 5;
    public int weakSpawnCount = 3;
    [SerializeField] private List<MeshFilter> rockMeshes;

    void Start()
    {
        foreach (var mesh in rockMeshes)
        {
            for (int i = 0; i < strongSpawnCount; i++)
            {
                Vector3 spawnPosition = GetRandomPointInMesh(mesh);
                var spawnedObject = Instantiate(strongMineSpot, spawnPosition, Quaternion.identity);
                spawnedObject.transform.SetParent(mesh.transform, true);
            }

            for (int i = 0; i < weakSpawnCount; i++)
            {
                Vector3 spawnPosition = GetRandomPointInMesh(mesh);
                var spawnedObject = Instantiate(weakMineSpot, spawnPosition, Quaternion.identity);
                spawnedObject.transform.SetParent(mesh.transform, true);
            }
        }
    }

    Vector3 GetRandomPointInMesh(MeshFilter meshFilter)
    {
        var mesh = meshFilter.mesh;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;

        int triangleIndex = Random.Range(0, triangles.Length / 3) * 3;

        Vector3 v0 = vertices[triangles[triangleIndex]];
        Vector3 v1 = vertices[triangles[triangleIndex + 1]];
        Vector3 v2 = vertices[triangles[triangleIndex + 2]];

        Transform meshTransform = meshFilter.transform;
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