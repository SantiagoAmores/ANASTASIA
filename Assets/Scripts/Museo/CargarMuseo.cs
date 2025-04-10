using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargarMuseo : MonoBehaviour
{
    public Button botonArma0;
    //public Button botonArma1;
    public Button botonArma2;
    public Button botonArma3;
    void Start()
    {
        Time.timeScale = 1f;

        WeaponManagerDDOL.instancia.armaSeleccionada = -1;
        botonArma0.onClick.RemoveAllListeners();
        //botonArma1.onClick.RemoveAllListeners();
        botonArma2.onClick.RemoveAllListeners();
        botonArma3.onClick.RemoveAllListeners();

        botonArma0.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma0());
        //botonArma1.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma1());
        botonArma2.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma2());
        botonArma3.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma3());
    }
}
