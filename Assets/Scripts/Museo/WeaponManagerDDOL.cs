using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagerDDOL : MonoBehaviour
{
    public static WeaponManagerDDOL instancia;

    public int armaSeleccionada = -1;

    public static bool cargarEscena = false;

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

    public void SeleccionarArma0() => SeleccionarArma(0);
    public void SeleccionarArma1() => SeleccionarArma(1);
    public void SeleccionarArma2() => SeleccionarArma(2);
    public void SeleccionarArma3() => SeleccionarArma(3);

    public void SeleccionarArma(int index)
    {
        Debug.Log("Arma numero " + index + " activada. Si pulsas empezar deberia activarse");
        armaSeleccionada = index;
    }
}
