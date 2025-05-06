using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoExp : MonoBehaviour
{
    // Script adicional para que cuando Anastasia se acerque a un punto de experiencia lo absorba

    private Transform anastasia;

    void Start()
    {
        anastasia = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (anastasia != null)
        {
            float distancia = Vector3.Distance(transform.position, anastasia.position);

            if (distancia <= 2f)
            {
                Vector3 direccion = (anastasia.position - transform.position).normalized;
                transform.position += direccion * 10f * Time.deltaTime;
            }
        }
    }
}
