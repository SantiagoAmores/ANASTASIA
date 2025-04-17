using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoExp : MonoBehaviour
{
    private Transform anastasia;

    private void Start()
    {
        anastasia = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
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
