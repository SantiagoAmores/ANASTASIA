using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoExp : MonoBehaviour
{
    // Script adicional para que cuando Anastasia se acerque a un punto de experiencia lo absorba

    private Transform anastasia;
    public static bool imanActivo = false;

    void Start()
    {
        anastasia = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (anastasia != null)
        {

            float rangoAtraccion = 2f;
            float velocidadAtraccion = 10f;

            if (imanActivo)
            {
                rangoAtraccion = 1000f;
                velocidadAtraccion = 30f;
            }

            float distancia = Vector3.Distance(transform.position, anastasia.position);

            if (distancia <= rangoAtraccion)
            {
                Vector3 direccion = (anastasia.position - transform.position).normalized;
                transform.position += direccion * velocidadAtraccion * Time.deltaTime;
            }
        }
    }
}
