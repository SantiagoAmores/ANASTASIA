using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe03_Llamas : MonoBehaviour
{
    public int daño = 10; // Puedes cambiarlo desde el inspector
    public float duracion = 5f;

    private void Start()
    {
        Destroy(gameObject, duracion);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovimientoJugador jugador = other.GetComponent<MovimientoJugador>();
            if (jugador != null)
            {
                jugador.herirAnastasia(daño); // Asumo que esta función ya existe
            }
        }
    }
}
