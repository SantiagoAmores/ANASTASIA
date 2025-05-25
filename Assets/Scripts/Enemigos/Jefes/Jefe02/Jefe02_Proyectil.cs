using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe02_Proyectil : MonoBehaviour
{
    public MovimientoJugador jugador;
    public GameObject posicionEfecto;

    private void Start()
    {
        jugador = FindObjectOfType<MovimientoJugador>();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            string nombreTostada = gameObject.name.Replace("(Clone)", "").Trim();
            if (nombreTostada == "TostadaBuena")
            {
                jugador.Curar(1);
            }

            else
            {
                jugador.herirAnastasia(1);
            }

            Destroy(gameObject);

        }
        
    }
}

