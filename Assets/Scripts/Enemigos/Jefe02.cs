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

    // Start is called before the first frame update
    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        statsScript = GetComponent<StatsEnemigos>();
        animator = GetComponentInChildren<Animator>();
        canvasPintura = FindObjectOfType<CanvasPintura>();
        statsScript.revisarEnemigo(); 
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
