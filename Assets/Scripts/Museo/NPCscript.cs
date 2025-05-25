using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCscript : MonoBehaviour
{
    public string categoriaDesbloqueo = "niveles";
    public int indiceDesbloqueo = 2;
    void Start()
    {
        if (!NivelManager.EstaDesbloqueado(categoriaDesbloqueo, indiceDesbloqueo))
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
