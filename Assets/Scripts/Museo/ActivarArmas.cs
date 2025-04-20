using Unity.Burst.Intrinsics;
using UnityEngine;

public class ActivadorDeArma : MonoBehaviour
{
    // Define las armas, es decir los scripts
    public MonoBehaviour arma1;
    public MonoBehaviour arma2;
    public MonoBehaviour arma3;
    public MonoBehaviour arma4;
    public MonoBehaviour arma5;
    public MonoBehaviour arma6;

    void Start()
    {
        // Si cargarEscena es false, ignora el start
        if (!WeaponManagerDDOL.cargarEscena)
        {
            return;
        }

        // Desactiva las armas
        arma1.enabled = false;
        arma2.enabled = false;
        arma3.enabled = false;
        arma4.enabled = false;
        arma5.enabled = false;
        arma6.enabled = false;
    }

    void Update()
    {
        // Si cargarEscena es false, ignora el start
        if (!WeaponManagerDDOL.cargarEscena)
        {
            return;
        }
        

        // Activa el arma
        int arma = WeaponManagerDDOL.instancia.armaSeleccionada;

        arma1.enabled = (arma == 0);
        arma2.enabled = (arma == 1);
        arma3.enabled = (arma == 2);
        arma4.enabled = (arma == 3);
        arma5.enabled = (arma == 2);
        arma6.enabled = (arma == 3);
    }
}