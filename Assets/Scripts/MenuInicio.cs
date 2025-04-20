using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    /*public GameObject opciones;
    public GameObject botonAtras;
    public GameObject botonSalirJuego;
    public GameObject menuPrincipal;*/

    public CinemachineVirtualCamera mainMenuCam;
    public CinemachineVirtualCamera optionsCam;

    // Start is called before the first frame update
    void Start()
    {
        /*menuPrincipal.SetActive(true);
        opciones.SetActive(false);
        botonAtras.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene("Scene_Museo");
        //SceneManager.LoadScene("Scene_Demo");
    }

    public void MostrarOpciones()
    {
        mainMenuCam.Priority = 0;
        optionsCam.Priority = 10;

        /*
        menuPrincipal.SetActive(false);
        opciones.SetActive(true);
        botonAtras.SetActive(true);*/
    }

    public void VolverMenu()
    {
        mainMenuCam.Priority = 10;
        optionsCam.Priority = 0;
        /*
        menuPrincipal.SetActive(true);
        botonAtras.SetActive(false);
        opciones.SetActive(false);*/
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
