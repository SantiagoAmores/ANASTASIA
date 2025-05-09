using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe03_Rayo : MonoBehaviour
{
    public int daño = 2;
    public float velocidadRotacion = 90f; // grados/segundo
    public float duracion = 2f;
    public float radio = 3f;
    public float anguloInicial = 0f;

    private Transform jugador;
    private GameObject contenedor;

    public bool girar = false;
    public Vector3 direccionInicial;

    private float tiempo = 0f;
    private Transform jefe;
    public float longitud = 7f; // largo del rayo desde el centro


    private void Start()
    {
        // Posicionar en el centro del jefe
        transform.position = jefe.position;

        // Orientar el rayo hacia adelante, y hacerlo salir desde el centro
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Escalar el rayo para que se extienda hacia afuera (eje Y es la longitud)
        transform.localScale = new Vector3(0.1f, 0f, 0.1f); // Y (longitud) en 0 para crecer luego
        StartCoroutine(CrecerRayo());

    }
    public void AsignarJefe(Transform jefeTransform)
    {
        jefe = jefeTransform;
    }


    void Update()
    {
        tiempo += Time.deltaTime;

        if (tiempo >= duracion)
        {
            Destroy(gameObject);
        }
        else if (girar)
        {
            // Mantener el rayo a la distancia correcta del jefe y hacerlo girar
            float anguloActual = anguloInicial + (velocidadRotacion * tiempo);
            Vector3 offset = new Vector3(
                Mathf.Sin(anguloActual * Mathf.Deg2Rad) * radio,
                0,
                Mathf.Cos(anguloActual * Mathf.Deg2Rad) * radio
            );
            transform.position = jefe.position + offset;
            transform.rotation = Quaternion.Euler(90f, anguloActual, 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovimientoJugador jugador = other.GetComponent<MovimientoJugador>();
            if (jugador != null)
            {
                jugador.herirAnastasia(daño);
            }
        }
    }
    private IEnumerator CrecerRayo()
    {
        float duracion = 0.5f; // Tiempo que tarda en crecer
        float tiempo = 0f;
        float escalaFinal = longitud;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float nuevaEscala = Mathf.Lerp(0f, escalaFinal, tiempo / duracion);
            transform.localScale = new Vector3(0.1f, nuevaEscala, 0.1f);
            yield return null;
        }

        // Asegurar la escala final exacta
        transform.localScale = new Vector3(0.1f, escalaFinal, 0.1f);

        // Después de escalar el rayo
        transform.localPosition += transform.up * (transform.localScale.y / 2f);

    }
}