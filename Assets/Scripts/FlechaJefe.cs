using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaJefe : MonoBehaviour
{
    private Transform jugador;
    private Transform jefe;
    public Vector3 offset = new Vector3(0, 0.1f, 0);

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        jefe = GameManager.instancia.jefeActual.transform;
        if (jefe == null || jugador == null)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!gameObject.activeSelf) { gameObject.SetActive(true); }

        transform.position = jugador.position + offset;
        Vector3 direccion = jefe.position - jugador.position;
        direccion.y = 0f;

        if (direccion.sqrMagnitude > 0.01f)
        {
            Quaternion rotacion = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 10f);
        }

    }
}
