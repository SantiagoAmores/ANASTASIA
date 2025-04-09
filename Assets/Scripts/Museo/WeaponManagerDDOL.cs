using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerDDOL : MonoBehaviour
{
    public static WeaponManagerDDOL instancia;

    public int armaSeleccionada = -1;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            armaSeleccionada = -1;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SeleccionarArma(int index)
    {
        armaSeleccionada = index;
    }
}
