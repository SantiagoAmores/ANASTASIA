using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Arma3 : MonoBehaviour
{
    // Jugador
    public GameObject player;

    // Proyectil
    public GameObject pintura;
    private float duracion = 1f;
    private float radio = 5f; // Área alrededor del jugador
    private Vector3 escalaFinal = new Vector3(5f, 0.1f, 5f);

    void Start()
    {
        player = GameObject.Find("Anastasia");
        StartCoroutine(InstanciarPintura());
    }

    private IEnumerator InstanciarPintura()
    {
        while (true)
        {

            Debug.Log("Instanciando pintura...");

            yield return new WaitForSeconds(1f);

            // Generar una posición aleatoria dentro de un cuadrado alrededor del jugador
            float randomX = Random.Range(-radio, radio);
            float randomZ = Random.Range(-radio, radio);

            Vector3 spawnPosition = player.transform.position + new Vector3(randomX, 0, randomZ);

            GameObject instanciarPintura = Instantiate(pintura, spawnPosition, Quaternion.identity);

            Vector3 escalaInicial = Vector3.zero;
            instanciarPintura.transform.localScale = escalaInicial;

            float tiempo = 0f;

            while (tiempo < duracion)
            {

                // Si no fue destruida antes la pintura
                if (instanciarPintura != null)
                {
                    instanciarPintura.transform.localScale = Vector3.Lerp(Vector3.zero, escalaFinal, tiempo / duracion);
                }
                else
                {
                    break; // Salimos del bucle si ya no existe
                }

                tiempo += Time.deltaTime;
                yield return null;
            }

            // Solo si aún existe, le damos la escala final y la destruimos después
            if (instanciarPintura != null)
            {
                instanciarPintura.transform.localScale = escalaFinal;
                Destroy(instanciarPintura, 0.2f);
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
