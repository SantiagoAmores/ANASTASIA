using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jefe02 : MonoBehaviour
{
    // FUNCIONALIDAD

    // Caminar por al escena
    // Pararse para disparar tostadas y pintura

    Enemigo enemigoScript;
    StatsEnemigos statsScript;
    Animator animator;
    public GameObject jugador;

    // Proyectiles
    public GameObject tostadaBuena;
    public GameObject tostadaMala;
    private CanvasPintura canvasPintura;

    private float cadenciaAtaque = 8f;
    private float tiempoEspera = 3f;

    // Puntos para lanzar proyectil
    public GameObject tostadaProyectil;
    public GameObject agujeroDerecha;
    public GameObject agujeroIzquierda;
    public GameObject particulas;

    // Start is called before the first frame update
    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        statsScript = GetComponent<StatsEnemigos>();
        animator = GetComponentInChildren<Animator>();
        canvasPintura = FindObjectOfType<CanvasPintura>();

        jugador = GameObject.FindGameObjectWithTag("Player");

        if (statsScript.faseDeJefe == 2)
        {
            transform.localScale *= 2f;
        }

        StartCoroutine(CicloAtaque());

    }

    void Update()
    {
        animator.SetFloat("velocidadActual", enemigoScript.enemigo.speed);
        enemigoScript.MirarAnastasia(); // Hacer que el jefe mire al jugador

    }

    public IEnumerator Atacar()
    {

        if (particulas != null)
        {
            GameObject particulasDerecha = Instantiate(particulas, agujeroDerecha.transform.position, Quaternion.LookRotation(Vector3.up));
            GameObject particulasIzquierda = Instantiate(particulas, agujeroIzquierda.transform.position, Quaternion.LookRotation(Vector3.up));

            Destroy(particulasDerecha, 5f);
            Destroy(particulasIzquierda, 5f);
        }

        // Animacion tostadas arriba
        GameObject instanciaTostadaDerecha = Instantiate(tostadaProyectil, agujeroDerecha.transform.position, Quaternion.LookRotation(Vector3.up));
        GameObject instanciaTostadaIzquierda = Instantiate(tostadaProyectil, agujeroIzquierda.transform.position, Quaternion.LookRotation(Vector3.up));

        Rigidbody rbDerecha = instanciaTostadaDerecha.GetComponent<Rigidbody>();
        //rbDerecha.constraints = RigidbodyConstraints.FreezePositionY;
        rbDerecha.AddForce(Vector3.up * 1500f);

        Rigidbody rbIzquierda = instanciaTostadaIzquierda.GetComponent<Rigidbody>();
        //rbIzquierda.constraints = RigidbodyConstraints.FreezePositionY;
        rbIzquierda.AddForce(Vector3.up * 1500f);

        StartCoroutine(DespawnProyectilRutina(instanciaTostadaDerecha));
        StartCoroutine(DespawnProyectilRutina(instanciaTostadaIzquierda));

        yield return new WaitForSeconds(1f);

        int cantidadProyectiles = 0;

        if (statsScript.faseDeJefe == 1)
        {
            cantidadProyectiles = 2;
            //canvasPintura.PrimeraFase();
        }
        else if (statsScript.faseDeJefe == 2)
        {
            cantidadProyectiles = 3;
            //canvasPintura.SegundaFase();
        }

        for (int i = 0; i < cantidadProyectiles; i++)
        {
            Vector3 randomPosition = GetRandomPositionOnNavMesh(transform.position, 15f);

            if (randomPosition == Vector3.zero) { continue; }

            GameObject tostadaBuenaInstanciada = Instantiate(tostadaBuena, randomPosition, Quaternion.identity);

            StartCoroutine(DespawnTostadaInstanciada(tostadaBuenaInstanciada));

        }

        for (int i = 0; i < cantidadProyectiles; i++)
        {
            Vector3 randomPosition = GetRandomPositionOnNavMesh(transform.position, 15f);

            if (randomPosition == Vector3.zero) { continue; }

            GameObject tostadaMalaInstanciada = Instantiate(tostadaMala, randomPosition, Quaternion.identity);

            StartCoroutine(DespawnTostadaInstanciada(tostadaMalaInstanciada));
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

    private IEnumerator CicloAtaque()
    {
        while (true)
        {
            statsScript.revisarEnemigo();

            // Persigue al jugador
            enemigoScript.enemigo.speed = statsScript.enemigoVelocidad;

            // Se detiene el movimiento
            yield return new WaitForSeconds(cadenciaAtaque);
            enemigoScript.enemigo.speed = 0f;

            // Ataca
            StartCoroutine(Atacar());

            // Espera unos segundos
            yield return new WaitForSeconds(tiempoEspera);

        }
    }

    private IEnumerator DespawnProyectilRutina(GameObject proyectilADespawnear)
    {
        yield return new WaitForSeconds(1f);
   
       Destroy(proyectilADespawnear);
    }

    private IEnumerator DespawnTostadaInstanciada(GameObject proyectilADespawnear)
    {
        yield return new WaitForSeconds(10f);

        Destroy(proyectilADespawnear);
    }

}
