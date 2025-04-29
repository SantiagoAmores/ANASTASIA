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

    public GameObject jefeProyectil;
    public List<GameObject> listaProyectiles = new List<GameObject>();
    private CanvasPintura canvasPintura;

    private float tiempoEntreAtaques = 5f;

    // Start is called before the first frame update
    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        statsScript = GetComponent<StatsEnemigos>();
        animator = GetComponentInChildren<Animator>();
        canvasPintura = FindObjectOfType<CanvasPintura>();
        statsScript.revisarEnemigo();

        StartCoroutine(CicloAtaque());
    }
    private IEnumerator CicloAtaque()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreAtaques);

            // Guardamos velocidad original y se detiene el movimiento
            float velocidadOriginal = enemigoScript.enemigo.speed;
            enemigoScript.enemigo.speed = 0f;

            // Ataca
            Atacar();

            // Espera unos segundos
            yield return new WaitForSeconds(1.5f);

            // Vuelve a perseguir a Anastasia
            enemigoScript.enemigo.speed = velocidadOriginal;
        }
    }

    public void Atacar()
    {
        // Detener movimiento
        statsScript.enemigoVelocidad = 0;

        int cantidadProyectiles = 0;

        if (statsScript.faseDeJefe == 1)
        {
            cantidadProyectiles = 2;
        }
        else if (statsScript.faseDeJefe == 2)
        {
            cantidadProyectiles = 4;
        }

        for (int i = 0; i < cantidadProyectiles; i++)
        {
            Vector3 offset = Random.insideUnitCircle * 3f;
            Vector3 destino = transform.position + new Vector3(offset.x, 0, offset.y);

            GameObject proyectil = Instantiate(jefeProyectil, transform.position + Vector3.up * 1f, Quaternion.identity);
            listaProyectiles.Add(proyectil);
            StartCoroutine(lanzarProyectil(proyectil, destino));
        }

        // Llamar a función del CanvasPintura para manchar la pantalla de pintura
        if (statsScript.faseDeJefe == 1)
        {
            canvasPintura.PrimeraFase();

        }
        else if (statsScript.faseDeJefe == 2)
        {
            canvasPintura.SegundaFase();
        }
    }
    private IEnumerator lanzarProyectil(GameObject proyectil, Vector3 destino)
    {
        float duracion = 0.5f;
        float tiempo = 0;
        Vector3 inicio = proyectil.transform.position;

        while (tiempo < duracion)
        {
            proyectil.transform.position = Vector3.Lerp(inicio, destino, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        proyectil.transform.position = destino;
        Destroy(proyectil, 1f);
    }
}
