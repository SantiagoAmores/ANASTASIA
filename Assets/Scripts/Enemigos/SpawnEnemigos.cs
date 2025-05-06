using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject[] enemigoPrefab;
    public bool seguir = true;
    public float spawnAreaRadius = 10f;
    public float tiempoEntreSpawns = 10f;

    private bool primerSpawn = true;

    public int ronda;

    private int minimoSpawns;
    private int maximoSpawns;

    public bool instanciar = true;

    private bool ronda2Empezada;

    void Start()
    {
        ronda = GetComponent<RoundManager>().ronda;
        minimoSpawns = 1;
        maximoSpawns = 3;
        StartCoroutine(Spawns());
    }

    private void Update()
    {
        if (ronda == 2 && !ronda2Empezada)
        {
            ronda2Empezada = true;
            primerSpawn = true;
            instanciar = true;
            minimoSpawns = 4;
            maximoSpawns = 8;
            StartCoroutine(Spawns());
        }
    }

    public IEnumerator Spawns()
    {
        while (seguir)
        {
            if (primerSpawn)
            {
                yield return new WaitForSeconds(tiempoEntreSpawns / 4);
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

                if (randomPosition == Vector3.zero) { continue; }

                int aleatorio = 0;

                switch (ronda)
                {
                    case 0:
                        aleatorio = (Random.value < 0.975f) ? 0 : 1;
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

                if (!instanciar) { continue; }
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

    public GameObject SpawnBoss(int fase)
    {
        Vector3 randomPositionBoss = GetRandomPositionOnNavMesh(transform.position, spawnAreaRadius);

        GameObject boss = Instantiate(enemigoPrefab[2], randomPositionBoss, Quaternion.identity);

        StatsEnemigos stats = boss.GetComponent<StatsEnemigos>();
        if (stats != null)
        {
            stats.faseDeJefe = fase;
            stats.revisarEnemigo();
        }
        return boss;
    }
}
