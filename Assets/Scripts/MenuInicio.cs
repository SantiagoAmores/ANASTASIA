using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public GameObject opciones;
    public GameObject botonAtras;
    public GameObject botonSalirJuego;
    public GameObject menuPrincipal;

    // Start is called before the first frame update
    void Start()
    {
        menuPrincipal.SetActive(true);
        opciones.SetActive(false);
        botonAtras.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene("ESCENA_HEIDI");
    }

    public void MostrarOpciones()
    {
        menuPrincipal.SetActive(false);
        opciones.SetActive(true);
        botonAtras.SetActive(true);
    }

    public void VolverMenu()
    {
        menuPrincipal.SetActive(true);
        botonAtras.SetActive(false);
        opciones.SetActive(false);
    }
}
