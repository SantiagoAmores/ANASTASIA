using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto1 : MonoBehaviour
{
    // Objeto 1 Iman: los puntos de experiencia son dirigidos a la posicion de Anastasia

    private Transform jugador;
    private Vector3 posicionInicial;
    private float tiempoInicio;
    private float duracion = 1f; // Tiempo que tarda en llegar al jugador

    private bool moviendo = false;

    void Start()
    {
        GameObject anastasia = GameObject.FindGameObjectWithTag("Player");
        if (anastasia != null)
        {
            jugador = anastasia.transform;
            posicionInicial = transform.position;
            tiempoInicio = Time.time;
            moviendo = true;
        }
    }

    void Update()
    {
        if (moviendo && jugador != null)
        {
            float t = (Time.time - tiempoInicio) / duracion;
            transform.position = Vector3.Lerp(posicionInicial, jugador.position, t);

            if (t >= 1f)
            {
                moviendo = false;
                Destroy(gameObject);
            }
        }
    }
}
