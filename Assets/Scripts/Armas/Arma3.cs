using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Arma3 : MonoBehaviour
{
    // Jugador
    public GameObject player;

    // Proyectil
    public GameObject pintura;
    private float duracion = 1f;
    private float distancia = 5f;
    private Vector3 escalaFinal = new Vector3(5f, 0.2f, 5f);

    void Start()
    {
        player = GameObject.Find("Jugador");
        StartCoroutine(InstanciarPintura());
    }

    private IEnumerator InstanciarPintura()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            GameObject instanciarPintura = Instantiate(pintura, transform.position, Quaternion.identity);

            Vector3 escalaInicial = Vector3.zero;
            instanciarPintura.transform.localScale = escalaInicial;

            float tiempo = 0f;

            while (tiempo < duracion)
            {
                instanciarPintura.transform.localScale = Vector3.Lerp(Vector3.zero, escalaFinal, tiempo / duracion);
                tiempo += Time.deltaTime;
                yield return null;
            }

            instanciarPintura.transform.localScale = escalaFinal;
            Destroy(instanciarPintura);

            yield return new WaitForSeconds(2f);
        }
    }
}
