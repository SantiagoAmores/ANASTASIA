using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma1 : MonoBehaviour
{
    public GameObject player;

    public GameObject[] enemigosLista;
    public GameObject enemigoMasCercano;

    public GameObject proyectil;
    public List<GameObject> proyectilLista = new List<GameObject>();

    void Start()
    {
        player = GameObject.Find("Anastasia");
        StartCoroutine("RutinaProyectil");

    }

    void Update()
    {
        enemigosLista = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public IEnumerator RutinaProyectil()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (enemigosLista.Length == 0) continue;
            EncontrarEnemigoMasCercano();
            if (enemigoMasCercano == null) continue;

            float distancia = Vector3.Distance(transform.position, enemigoMasCercano.transform.position);

            if (distancia <= 6f)
            {
                DispararProyectil(enemigoMasCercano);
            }
        }
    }

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

    private void DispararProyectil(GameObject objetivo)
    {
        GameObject instanciaProyectil = Instantiate(proyectil, transform.position, Quaternion.identity);
        proyectilLista.Add(instanciaProyectil);

        Rigidbody rb = instanciaProyectil.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY;
        rb.AddForce((objetivo.transform.position - transform.position).normalized * 400f);

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
