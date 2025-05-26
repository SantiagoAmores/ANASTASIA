using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cuadroScript : MonoBehaviour
{
    public bool nivelSuperado;
    public int nivelASuperar;
    public Material cuadroMalo;
    public Material cuadroBueno;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (NivelManager.EstaDesbloqueado("niveles", nivelASuperar))
        {
            nivelSuperado = true;
        }
        else
        {
            nivelSuperado = false;
        }

        Material[] materiales = renderer.materials;

        if (nivelSuperado)
        {
            materiales[1] = cuadroBueno;
            renderer.materials = materiales;
        }
        else
        {
            materiales[1] = cuadroMalo;
            renderer.materials = materiales;
        }
    }
}
