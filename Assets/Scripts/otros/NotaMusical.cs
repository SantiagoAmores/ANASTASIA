using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotaMusical : MonoBehaviour
{
    public float velocidadFlotacion = 1f;
    public float amplitud = 0.05f;

    private Vector3 posicionBase;
    private bool posicionGuardada = false;

    void Awake()
    {
        // Guardar la posición original solo una vez
        posicionBase = transform.position;
        posicionGuardada = true;
    }

    void Update()
    {
        if (!posicionGuardada) return;

        float offset = Mathf.Sin(Time.time * velocidadFlotacion) * amplitud;
        transform.position = posicionBase + new Vector3(0, offset, 0);
    }
}
