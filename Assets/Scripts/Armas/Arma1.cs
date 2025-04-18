using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arma1 : MonoBehaviour
{
    public GameObject[] enemigosLista;
    public GameObject enemigoMasCercano;

    public GameObject proyectil;
    public List<GameObject> proyectilLista = new List<GameObject>();

    public StatsAnastasia stats;

    void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<StatsAnastasia>();
        StartCoroutine("RutinaProyectil");
    }

    // Actualiza la lista de enemigos
    void Update()
    {
        enemigosLista = GameObject.FindGameObjectsWithTag("Enemy");
    }


    // Dispara cada cierta cantidad de tiempo y si hay enemigos busca al mas cercano
    // Calcula la distancia entre el jugador y el enemigo para que dispare a partir de cierto rango
    public IEnumerator RutinaProyectil()
    {
        while (true)
        {
            yield return new WaitForSeconds(stats.arma1Cadencia);

            if (enemigosLista.Length == 0) continue;
            EncontrarEnemigoMasCercano();
            if (enemigoMasCercano == null) continue;

            float distancia = Vector3.Distance(transform.position, enemigoMasCercano.transform.position);
            if (distancia <= 9f) { DispararProyectil(enemigoMasCercano); }
        }
    }

    // Calcula cual es el enemigo mas cercano al jugador calculando la distancia entre el jugador y los enemigos
    private void EncontrarEnemigoMasCercano()
    {
        enemigoMasCercano = null;
        float distanciaMasCercana = float.MaxValue;

        foreach (GameObject enemigo in enemigosLista)
        {
            float distancia = (enemigo.transform.position - transform.position).sqrMagnitude;
            if (distancia < distanciaMasCercana)
            {
                enemigoMasCercano = enemigo;
                distanciaMasCercana = distancia;
            }
        }
    }

    // Instancia un proyectil y adicionalmente lo añade a una lista por si el proyectil no impacta que desaparezca tras cierto tiempo
    // Ademas, le añade fuerza al proyectil para que se dispare en direccion al enemigo mas cercano
    private void DispararProyectil(GameObject objetivo)
    {
        Vector3 direccion = (objetivo.transform.position -transform.position).normalized;

        GameObject instanciaProyectil = Instantiate(proyectil, transform.position, Quaternion.LookRotation(direccion));
        proyectilLista.Add(instanciaProyectil);

        Rigidbody rb = instanciaProyectil.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.AddForce(direccion * 500f);

        // CALCULA EL DAÑO DEL PROYECTIL
        ProyectilDestruible proyectilScript = instanciaProyectil.GetComponent<ProyectilDestruible>();
        if (proyectilScript != null) { proyectilScript.golpe = (int)stats.arma1Ataque; }

        StartCoroutine(DespawnProyectilRutina(instanciaProyectil));
    }

    private IEnumerator DespawnProyectilRutina(GameObject proyectilADespawnear)
    {
        yield return new WaitForSeconds(2f);
        if (proyectilLista.Count > 0)
        {
            Destroy(proyectilLista[0]);
            proyectilLista.RemoveAt(0);
        }
    }
}
