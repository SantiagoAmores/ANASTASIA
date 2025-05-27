using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma6 : MonoBehaviour
{
    public GameObject gatoPrefab;
    public StatsAnastasia stats;
    private MovimientoJugador movimientoJugador;

    private void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<StatsAnastasia>();
        movimientoJugador = GameObject.FindWithTag("Player").GetComponent<MovimientoJugador>();
        StartCoroutine(RutinaDisparo());
    }

    IEnumerator RutinaDisparo()
    {
        while (true)
        {
            if (movimientoJugador.partidaTerminada) yield break;

            yield return new WaitForSeconds(stats.arma6Cadencia);

            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) continue;

            DispararProyectil();
        }
    }

    void DispararProyectil()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject objetivo = enemigos[Random.Range(0, enemigos.Length)];

        GameObject gato = Instantiate(gatoPrefab, transform.position, Quaternion.identity);
        gato.layer = LayerMask.NameToLayer("Gato");
        GatoRebota gatoScript = gato.GetComponent<GatoRebota>();

        if (gatoScript != null)
        {
            gatoScript.golpe = (int)StatsAnastasia.arma6Ataque;
            gatoScript.direccion = (objetivo.transform.position - transform.position).normalized;
        }
    }

    private void Update()
    {
        if (movimientoJugador.partidaTerminada)
        {
            StopAllCoroutines();
        }
    }
}
