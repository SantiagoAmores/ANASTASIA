using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo; // El transform del personaje que quieres seguir

    private Vector3 offset; // La diferencia de posición inicial entre la cámara y el personaje

    void Start()
    {
        offset = transform.position - objetivo.position; // Calcula la diferencia de posición inicial
    }

    void FixedUpdate()
    {
        // Obtiene la nueva posición de la cámara basada en la posición del personaje más el offset
        Vector3 nuevaPosicion = objetivo.position + offset;
        transform.position = nuevaPosicion;

    }
}
