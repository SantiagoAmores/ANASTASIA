using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnJarron : MonoBehaviour
{
    public GameObject[] enemigoPrefab;
    public float spawnAreaRadius = 10f;
    public float tiempoEntreSpawns = 30f;

    public GameObject spawnEfectoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnJarrones());
    }

    IEnumerator spawnJarrones()
    {
        while (true)
        { 
            yield return new WaitForSeconds(tiempoEntreSpawns);

            Vector3 randomPosition = GetRandomPositionOnNavMesh(transform.position, spawnAreaRadius);
            Instantiate(enemigoPrefab[0], randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionOnNavMesh(Vector3 center, float radius)
    {
        Vector2 randomCircle = Random.insideUnitCircle * radius;
        Vector3 randomPosition = center + new Vector3(randomCircle.x, 0f, randomCircle.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }
}
