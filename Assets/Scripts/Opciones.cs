using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
    public Image panelBrillo;
    public Slider sliderBrillo;
    public Toggle pantallaCompletaToggle;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(panelBrillo);

        //Brillo
        float brilloGuardado = PlayerPrefs.GetFloat("brillo", 1f);
        sliderBrillo.value = brilloGuardado;
        CambiarBrillo(brilloGuardado);

        //Pantalla completa
        bool pantallaCompleta = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;
        pantallaCompletaToggle.isOn = pantallaCompleta;
        CambiarPantallaCompleta(pantallaCompleta);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarBrillo (float valor)
    {
        Color colorActual = panelBrillo.color;
        colorActual.a = 1f - valor;
        panelBrillo.color = colorActual;

        PlayerPrefs.SetFloat("brillo", valor);
    }

    public void CambiarPantallaCompleta (bool esCompleta)
    {
        Screen.fullScreen = esCompleta;
        PlayerPrefs.SetInt("pantallaCompleta", esCompleta ? 1 : 0);
    }
}
