using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject[] enemigoPrefab;
    public bool seguir = true;
    public float spawnAreaRadius = 10f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawns());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator Spawns()
    {
        while (seguir)
        {
            yield return new WaitForSeconds(1f);

            Vector3 randomPosition = GetRandomPositionOnNavMesh(transform.position, spawnAreaRadius);
            
            if(randomPosition != Vector3.zero)
            {
                int aleatorio = Random.Range(0, 5);
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
}
