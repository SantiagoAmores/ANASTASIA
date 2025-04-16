using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject[] enemigoPrefab;
    public bool seguir = true;
    public float spawnAreaRadius = 10f;

    public GameObject spawnEfectoPrefab;

    public float tiempoEntreSpawns = 5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawns());
        StartCoroutine(CambiarRatioEnemigos());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Spawns()
    {
        while (seguir)
        {
            yield return new WaitForSeconds(tiempoEntreSpawns);

            Vector3 randomPosition = GetRandomPositionOnNavMesh(transform.position, spawnAreaRadius);

            if (spawnEfectoPrefab != null)
            {
                GameObject efecto = Instantiate(spawnEfectoPrefab, randomPosition, Quaternion.identity);
                Destroy(efecto, 1f);
            }
            
            if(randomPosition != Vector3.zero)
            {
                int aleatorio = Random.Range(0, 6);
                Instantiate(enemigoPrefab[aleatorio], randomPosition, Quaternion.identity);
            }
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

    public IEnumerator CambiarRatioEnemigos()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            tiempoEntreSpawns = Mathf.Max(0.1f, tiempoEntreSpawns/2f);
        }
    }
}
