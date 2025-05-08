using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe02 : MonoBehaviour
{
    // FUNCIONALIDAD

    // Caminar por al escena
    // Pararse para disparar tostadas y pintura


    Enemigo enemigoScript;
    StatsEnemigos statsScript;
    Animator animator;
    public GameObject jugador;

    public List<GameObject> listaProyectiles = new List<GameObject>();
    private CanvasPintura canvasPintura;

    private float cadenciaAtaque = 8f;
    private float tiempoEspera = 1f;

    // Start is called before the first frame update
    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        statsScript = GetComponent<StatsEnemigos>();
        animator = GetComponentInChildren<Animator>();
        canvasPintura = FindObjectOfType<CanvasPintura>();
        statsScript.revisarEnemigo();

        jugador = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(CicloAtaque());
    }

    void Update()
    {
        animator.SetFloat("velocidadActual", enemigoScript.enemigo.speed);
        enemigoScript.MirarAnastasia(); // Hacer que el jefe mire al jugador

        // Se mueve hacia el jugador mientras no este atacando
        if (enemigoScript.enemigo.speed > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, jugador.transform.position, enemigoScript.enemigo.speed * Time.deltaTime);
        }
    }

    public void Atacar()
    {
        // Detener movimiento
        statsScript.enemigoVelocidad = 0f;

        int cantidadProyectiles = 0;

        if (statsScript.faseDeJefe == 1)
        {
            cantidadProyectiles = 4;
        }
        else if (statsScript.faseDeJefe == 2)
        {
            cantidadProyectiles = 6;
        }

        for (int i = 0; i < cantidadProyectiles; i++)
        {
            Vector3 offset = Random.insideUnitCircle * 3f;
            Vector3 destino = transform.position + new Vector3(offset.x, 0, offset.y);

            int indice = Random.Range(0, listaProyectiles.Count);
            GameObject prefabElegido = listaProyectiles[indice];
            GameObject proyectil = Instantiate(prefabElegido, transform.position + Vector3.up * 1f, Quaternion.identity);
            StartCoroutine(lanzarProyectil(proyectil, destino));
        }

        // Llamar a función del CanvasPintura para manchar la pantalla de pintura
        //if (statsScript.faseDeJefe == 1)
        //{
        //    canvasPintura.PrimeraFase();

        //}
        //else if (statsScript.faseDeJefe == 2)
        //{
        //    canvasPintura.SegundaFase();
        //}
    }

    private IEnumerator CicloAtaque()
    {
        while (true)
        {
            // Persigue al jugador
            enemigoScript.enemigo.speed = statsScript.enemigoVelocidad;

            // Guardamos velocidad original y se detiene el movimiento
            yield return new WaitForSeconds(cadenciaAtaque);
            float velocidadOriginal = enemigoScript.enemigo.speed;
            enemigoScript.enemigo.speed = 0f;

            // Ataca
            Atacar();

            // Espera unos segundos
            yield return new WaitForSeconds(tiempoEspera);

            // Vuelve a perseguir a Anastasia
            enemigoScript.enemigo.speed = velocidadOriginal;
        }
    }

    private IEnumerator lanzarProyectil(GameObject proyectil, Vector3 destino)
    {
        float duracionSubida = 0.5f;  // Tiempo que tarda en subir
        float duracionCaida = 2f;  // Tiempo que tarda en caer

        // Posicion inicial del proyectil
        Vector3 inicio = proyectil.transform.position;
        Vector3 destinoFinal = new Vector3(destino.x, inicio.y, destino.z); // Solo movemos en X y Z

        // Subida del proyectil
        float tiempoTranscurrido = 0;
        while (tiempoTranscurrido < duracionSubida)
        {
            tiempoTranscurrido += Time.deltaTime;
            proyectil.transform.position = Vector3.Lerp(inicio, new Vector3(inicio.x, inicio.y + 2f, inicio.z), tiempoTranscurrido / duracionSubida);
            yield return null;
        }

        // Caida hacia el destino final
        tiempoTranscurrido = 0;
        while (tiempoTranscurrido < duracionCaida)
        {
            tiempoTranscurrido += Time.deltaTime;
            proyectil.transform.position = Vector3.Lerp(new Vector3(inicio.x, inicio.y + 2f, inicio.z), destinoFinal, tiempoTranscurrido / duracionCaida);
            yield return null;
        }

        // Asegurar que el proyectil llegue exactamente al destino
        proyectil.transform.position = destinoFinal;
  
    }
}
