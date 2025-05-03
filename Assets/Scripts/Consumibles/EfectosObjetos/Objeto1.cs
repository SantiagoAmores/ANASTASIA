using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Objeto1 : MonoBehaviour
{
    // Objeto 1 Iman: los puntos de experiencia son dirigidos a la posicion de Anastasia

    private Transform jugador;
    private GameObject[] puntosExperiencia;
    public float duracion = 1f; // Tiempo que tarda en llegar al jugador

    void Start()
    {
        GameObject anastasia = GameObject.FindGameObjectWithTag("Player");
        jugador = anastasia.transform; //Posicion del jugador
        
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {

            UsarObjeto(); //Activamos el efecto pulsando E
        }
    }

    void UsarObjeto()
    {
        //Buscar todos los objetos con el tag experiencia
        GameObject[] puntosExp = GameObject.FindGameObjectsWithTag("Experiencia");

        foreach (GameObject exp in puntosExperiencia)
        {
            StartCoroutine(AtraerExperiencia(exp));
        }

        // Desactivar el script del objeto temporalmente mientras se mueve la experiencia
        StartCoroutine(EsperarYDestruir());
    }

    IEnumerator AtraerExperiencia(GameObject experiencia)
    {
        Vector3 posicionInicial = experiencia.transform.position;
        float tiempoInicio = Time.time;

        // Mover el objeto de experiencia hacia el jugador
        while (Vector3.Distance(experiencia.transform.position, jugador.position) > 0.1f)
        {
            float t = (Time.time - tiempoInicio) / duracion;
            experiencia.transform.position = Vector3.Lerp(posicionInicial, jugador.position, t);

            yield return null; // Esperar el siguiente frame
        }
    }

    // Corutina para esperar a que todos los objetos lleguen antes de destruir el imán
    IEnumerator EsperarYDestruir()
    {
        // Esperamos el tiempo que tarda
        yield return new WaitForSeconds(2f);

        // Y se destruye el iman
        Destroy(gameObject);
    }
}
