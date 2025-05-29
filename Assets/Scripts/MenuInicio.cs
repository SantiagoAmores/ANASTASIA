using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class MenuInicio : MonoBehaviour
{
    /*public GameObject opciones;
    public GameObject botonAtras;
    public GameObject botonSalirJuego;
    public GameObject menuPrincipal;*/

    public CinemachineVirtualCamera mainMenuCam;
    public CinemachineVirtualCamera optionsCam;
    public CinemachineVirtualCamera creditosCam;

    public GameObject panelCreditos;
    public GameObject panelConfirmacionSalir;

    public CreditosLoop creditosLoop;

    public CinematicaControler cinematicController;

    public float normalSpeed = 40f;
    public float fastSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        /*menuPrincipal.SetActive(true);
        opciones.SetActive(false);
        botonAtras.SetActive(false);*/

        panelCreditos.SetActive(false);
        panelConfirmacionSalir.SetActive(false);
        creditosLoop.SetSpeed(normalSpeed); // Velocidad inicial
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarJuego()
    {
        if (PlayerPrefs.HasKey("PrimeraVez"))
            SceneManager.LoadScene("Scene_Museo");
        else
            SceneManager.LoadScene("Scene_Cinematica");
    }


    public void MostrarOpciones()
    {
        mainMenuCam.Priority = 0;
        optionsCam.Priority = 10;
        creditosCam.Priority = 0;

        panelCreditos.SetActive(false);

        /*
        menuPrincipal.SetActive(false);
        opciones.SetActive(true);
        botonAtras.SetActive(true);*/
    }

    public void MostrarCreditos()
    {
        mainMenuCam.Priority = 0;
        optionsCam.Priority = 0;
        creditosCam.Priority = 10;

        panelCreditos.SetActive(true);
        creditosLoop.SetSpeed(normalSpeed); // Reiniciar a velocidad normal por si acaso
    }

    public void VolverMenu()
    {
        mainMenuCam.Priority = 10;
        optionsCam.Priority = 0;
        creditosCam.Priority = 0;

        /*
        menuPrincipal.SetActive(true);
        botonAtras.SetActive(false);
        opciones.SetActive(false);*/
    }

    public void SalirJuego()
    {
        panelConfirmacionSalir.SetActive(true);
    }

    public void ConfirmarSalir()
    {
        Application.Quit();
    }

    public void CancelarSalir()
    {
        panelConfirmacionSalir.SetActive(false);
    }



    // Métodos para acelerar/desacelerar los créditos con el botón UI
    public void AcelerarCreditos()
    {
        creditosLoop.SetSpeed(fastSpeed);
    }

    public void FrenarCreditos()
    {
        creditosLoop.SetSpeed(normalSpeed);
    }
}
