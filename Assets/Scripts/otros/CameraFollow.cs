using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo; // El transform del personaje que quieres seguir

    private Vector3 offset; // La diferencia de posici�n inicial entre la c�mara y el personaje

    void Start()
    {
        offset = transform.position - objetivo.position; // Calcula la diferencia de posici�n inicial
    }

    void FixedUpdate()
    {
        // Obtiene la nueva posici�n de la c�mara basada en la posici�n del personaje m�s el offset
        Vector3 nuevaPosicion = objetivo.position + offset;
        transform.position = nuevaPosicion;

    }
}
