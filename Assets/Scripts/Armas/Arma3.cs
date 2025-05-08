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
    public GameObject cuboPintura;

    //Parametros
    private float duracion = 1f;
    private float radio = 5f; // Área alrededor del jugador
    private Vector3 escalaFinal = new Vector3(5f, 0.01f, 5f);

    // Stats
    public StatsAnastasia stats;

    void Start()
    {
        player = GameObject.Find("Anastasia");
        stats = GameObject.FindWithTag("Player").GetComponent<StatsAnastasia>();
        StartCoroutine(InstanciarPintura());
    }

    private IEnumerator InstanciarPintura()
    {
        while (true)
        {

            // Comprobamos que funciona el bucle
            //Debug.Log("Instanciando pintura...");

            yield return new WaitForSeconds(stats.arma3Cadencia);

            // Generar una posición aleatoria dentro de un cuadrado alrededor del jugador
            float randomX = Random.Range(-radio, radio);
            float randomZ = Random.Range(-radio, radio);

            Vector3 spawnPosition = player.transform.position + new Vector3(randomX, -1f, randomZ);

            // Instanciar charco de pintura
            GameObject instanciarPintura = Instantiate(pintura, spawnPosition, Quaternion.identity);

            // Instanciar cubo de pintura
            if (cuboPintura != null)
            {
                Vector3 cuboPosition = spawnPosition + new Vector3(0, 5f, 0); // Posicion mas arriba de donde se situa el charco
                GameObject cuboInstancia = Instantiate(cuboPintura, cuboPosition, Quaternion.identity);
                StartCoroutine(RotarCubo(cuboInstancia)); // Animacion del arma
                Destroy(cuboInstancia, 1.5f); // Destruir el cubo después de que termine la animación
            }

            // Se calcula el dano del charco de pintura
            CharcoPintura charcoScript = instanciarPintura.GetComponent<CharcoPintura>();
            if (charcoScript != null) { charcoScript.golpe = 1; }

            // Escalado d ela pintura
            Vector3 escalaInicial = Vector3.zero;
            instanciarPintura.transform.localScale = escalaInicial;

            float alturaFija = escalaFinal.y;
            Vector3 escalaTarget = new Vector3(
                escalaFinal.x * (1 + stats.arma4Ataque / 7f),
                alturaFija,
                escalaFinal.z * (1 + stats.arma4Ataque / 7f)
                );

            float tiempo = 0f;
            while (tiempo < duracion)
            {

                // Si no fue destruida antes la pintura
                if (instanciarPintura != null)
                {
                    instanciarPintura.transform.localScale = Vector3.Lerp(Vector3.zero, escalaTarget, tiempo / duracion);
                }
                else
                {
                    break; // Salimos del bucle si ya no existe
                }

                tiempo += Time.deltaTime;
                yield return null;
            }

            // Solo si aun existe, le damos la escala final y la destruimos después
            if (instanciarPintura != null)
            {
                instanciarPintura.transform.localScale = escalaTarget;
                Destroy(instanciarPintura, 2f + stats.arma3Ataque/5f);
            }

        }
    }
    private IEnumerator RotarCubo(GameObject cubo)
    {
        // Rotacion
        Quaternion rotacionInicial = cubo.transform.rotation;
        Quaternion rotacionFinal = Quaternion.Euler(180f, 0f, 0f); // Girarlo 180 grados en el eje X

        float duracionRotacion = 0.5f; // Tiempo para que se complete la rotacion
        float tiempo = 0f;

        while (tiempo < duracionRotacion)
        {
            if (cubo != null)
            {
                cubo.transform.rotation = Quaternion.Lerp(rotacionInicial, rotacionFinal, tiempo / duracionRotacion);
            }
            else
            {
                break;
            }

            tiempo += Time.deltaTime;
            yield return null;
        }

        if (cubo != null)
        {
            cubo.transform.rotation = rotacionFinal;
        }
    }
}
