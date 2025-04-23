using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe01_Proyectil : MonoBehaviour
{
    public StatsEnemigos statsScript;
    public GameObject jugador;
    public MovimientoJugador movimientoScript;
    public GameObject particulasProyectilJefe03;

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        movimientoScript = jugador.GetComponent<MovimientoJugador>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject particulas = Instantiate(particulasProyectilJefe03, transform.position, transform.rotation);

            if (statsScript.faseDeJefe == 2)
            {
                particulas.transform.localScale *= 2;
            }

            movimientoScript.herirAnastasia(1);
            Destroy(particulas, 2f);

            Destroy(gameObject);
        }
    }
}
