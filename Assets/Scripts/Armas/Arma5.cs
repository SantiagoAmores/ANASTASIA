using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma5 : MonoBehaviour
{
    // Jugador
    public GameObject player;

    // Proyectil
    public GameObject rastroPintura;
    public GameObject rodilloPrefab;

    // Parametros
    private bool activado = true;
    private float intervaloInstanciacion = 0.2f; // Tiempo entre instanciacion
    private float duracion = 2f; // Duracion de cada charco
    private Vector3 ultimaPosicion; // Saber si se mueve
    private float umbralMovimiento = 0.01f; // Para evitar errores
    private float cadencia; // Tiempo entre cada activación del arma

    // Stats
    public StatsAnastasia stats;

    void Start()
    {
        player = GameObject.Find("Anastasia");
        stats = GameObject.FindWithTag("Player").GetComponent<StatsAnastasia>();
        ultimaPosicion = player.transform.position; // Se guarda la posicion inicial
        cadencia = StatsAnastasia.arma5CadenciaBase; // Usamos la cadencia asignada al Arma 5
        StartCoroutine(ActivarRastro());
    }

    private IEnumerator ActivarRastro()
    {
        while (true)
        {
            yield return new WaitForSeconds(cadencia); // Espera el tiempo especificado por la cadencia

            activado = true;
            Quaternion rotacionJugador = player.transform.rotation; // Siempre quede mirando a las espaldas
            Quaternion rotacionRodillo = Quaternion.Euler(-90f, rotacionJugador.eulerAngles.y, 0f); // Rotacion del rodillo
            GameObject rodillo = Instantiate(rodilloPrefab, ObtenerPosicionDetras(), rotacionRodillo); // Instanciamos el rodillo
            rodillo.transform.parent = player.transform; // Hacemos que el rodillo se mueva con el jugador

            StartCoroutine(GenerarRastro());

            yield return new WaitForSeconds(2f); // Tiempo en el que se genera el rastro
            activado = false; // Se desactiva el arma

            // Destruimos el rodillo después de un tiempo (igual que el rastro)
            Destroy(rodillo);
        }
    }
    private IEnumerator GenerarRastro()
    {
        while (true)
        {
            if (activado && SeEstaMoviendo())
            {
                // Posicion rastro detras de Anastasia
                Vector3 posicionRastro = ObtenerPosicionDetras();

                // Obtener la rotación del jugador en el eje Y (vertical)
                Quaternion rotacionRastro = Quaternion.Euler(0f, player.transform.eulerAngles.y, 0f);

                // Instaciar rastro
                GameObject nuevoCharco = Instantiate(rastroPintura, posicionRastro, rotacionRastro);

                // Asignar dano del rastro
                CharcoPintura charcoScript = nuevoCharco.GetComponent<CharcoPintura>();
                if (charcoScript != null)
                {
                    charcoScript.golpe = (int)stats.arma5Ataque;
                }

                Destroy(nuevoCharco, duracion);
            }

            // Se guarda la posicion actual para la siguiente comparacion
            ultimaPosicion = player.transform.position;

            yield return new WaitForSeconds(intervaloInstanciacion);
        }
    }

    private bool SeEstaMoviendo()
    {
        // Compara la distancia entre la ultima posicion y la actual
        float distancia = Vector3.Distance(player.transform.position, ultimaPosicion);
        return distancia > umbralMovimiento;
    }

    private Vector3 ObtenerPosicionDetras()
    {
        // Direccion detras de Anastasia
        Vector3 direccionDetras = -player.transform.forward; // Direccion en la que esta mirando el jugador
        return player.transform.position + direccionDetras * 1f + new Vector3(0, -1f, 0); // Posicion detras y en el suelo
    }
}
