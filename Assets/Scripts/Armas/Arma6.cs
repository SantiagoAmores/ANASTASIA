using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma6 : MonoBehaviour
{
    public GameObject gatoPrefab;
    public StatsAnastasia stats;

    private void Start()
    {
        stats = GameObject.FindWithTag("Player").GetComponent<StatsAnastasia>();
        StartCoroutine(RutinaDisparo());
    }

    IEnumerator RutinaDisparo()
    {
        while (true)
        {
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
        GatoRebota gatoScript = gato.GetComponent<GatoRebota>();

        if (gatoScript != null)
        {
            gatoScript.golpe = (int)stats.arma6Ataque;
            gatoScript.direccion = (objetivo.transform.position - transform.position).normalized;
        }
    }
}
