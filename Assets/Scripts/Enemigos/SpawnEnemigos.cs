using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemigos : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] enemigoPrefab;

    [Header("Spawn Configuración")]
    private float spawnAreaRadius = 50f;
    private float tiempoEntreSpawns = 5f;
    public bool seguir = true;
    public bool instanciar = true;

    [Header("Rondas")]
    private RoundManager roundManager;
    private bool ronda2Empezada;

    [Header("Spawn Adicionales")]
    private bool primerSpawn = true;
    private int minimoSpawns = 2;
    private int maximoSpawns = 4;

    void Start()
    {
        roundManager = GetComponent<RoundManager>();
        StartCoroutine(Spawns());
    }

    private void Update()
    {
        if (roundManager.ronda == 2 && !ronda2Empezada)
        {
            ronda2Empezada = true;
            primerSpawn = true;
            instanciar = true;
            tiempoEntreSpawns /= 2;
            minimoSpawns = 4;
            maximoSpawns = 7;
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
            else {  yield return new WaitForSeconds(tiempoEntreSpawns); }

            int cantidadEnemigos = Random.Range(minimoSpawns,maximoSpawns);

            for (int i = 0; i < cantidadEnemigos; i++)
            {
                Vector3 randomPosition = GetRandomPositionOnNavMesh(transform.position, spawnAreaRadius);

                if (randomPosition == Vector3.zero) { continue; }

                int aleatorio = TipoDeEnemigo();

                if (!instanciar) { continue; }
                GameObject enemigo = Instantiate(enemigoPrefab[aleatorio], randomPosition, Quaternion.identity);
            }
        }
    }

    private int TipoDeEnemigo()
    {
        switch (roundManager.ronda)
        {
            case 0:
                return (Random.value < 0.975f) ? 0 : 1;
            case 1:
                instanciar = false;
                return 0;
            case 2:
                return (Random.value < 0.3f) ? 0 : 1;
            case 3:
                instanciar = false;
                return 0;
            default:
                return Random.Range(0, 2);
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
