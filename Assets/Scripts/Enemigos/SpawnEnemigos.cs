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

    public float tiempoEntreSpawns = 6f;

    private bool primerSpawn = true;

    public int rondaTemporal;

    private int minimoSpawns;
    private int maximoSpawns;

    void Start()
    {
        minimoSpawns = 2;
        maximoSpawns = 5;
        StartCoroutine(Spawns());
    }

    private void Update()
    {
        minimoSpawns = 3;
        maximoSpawns = 6;
    }

    public IEnumerator Spawns()
    {
        while (seguir)
        {
            if (primerSpawn)
            {
                yield return new WaitForSeconds(tiempoEntreSpawns / 2);
                primerSpawn = false;
            }
            else
            {
                yield return new WaitForSeconds(tiempoEntreSpawns);
            }

            int cantidadEnemigos = Random.Range(minimoSpawns,maximoSpawns);

            for (int i = 0; i < cantidadEnemigos; i++)
            {
                Vector3 randomPosition = GetRandomPositionOnNavMesh(transform.position, spawnAreaRadius);

                if (randomPosition == Vector3.zero)
                {
                    continue;
                }

                int aleatorio = 0;
                bool instanciar = true;

                switch (rondaTemporal)
                {
                    case 0:
                        aleatorio = (Random.value < 0.8f) ? 0 : 1;
                        break;
                    case 1:
                        instanciar = false;
                        break;
                    case 2:
                        aleatorio = (Random.value < 0.2f) ? 0 : 1;
                        break;
                    case 3:
                        instanciar = false;
                        break;
                    default:
                        aleatorio = Random.Range(0,2);
                        break;
                }

                if (!instanciar)
                {
                    continue;
                }

                if (spawnEfectoPrefab != null)
                {
                    GameObject efecto = Instantiate(spawnEfectoPrefab, randomPosition, Quaternion.identity);
                    Destroy(efecto, 1f);
                }

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

    public void SpawnBoss(int fase)
    {
        Vector3 randomPositionBoss = GetRandomPositionOnNavMesh(transform.position, spawnAreaRadius);

        GameObject boss = Instantiate(enemigoPrefab[2], randomPositionBoss, Quaternion.identity);

        if (spawnEfectoPrefab != null)
        {
            GameObject efecto = Instantiate(spawnEfectoPrefab, randomPositionBoss, Quaternion.identity);
            Destroy(efecto, 1f);
        }

        StatsEnemigos stats = boss.GetComponent<StatsEnemigos>();
        if (stats != null)
        {
            stats.faseDeJefe = fase;
            stats.revisarEnemigo();
        }
    }
}
