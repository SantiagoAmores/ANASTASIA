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

    public static Opciones patatilla;

    /*void Awake()
    {
        if (patatilla == null)
        {
            patatilla = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/

    // Start is called before the first frame update
    void Start()
    {
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

    public void CambiarBrillo(float valor)
    {
        // Cambiar el alpha del panel
        Color colorActual = panelBrillo.color;
        colorActual.a = 1f - valor;
        panelBrillo.color = colorActual;

        // Guardar la configuración
        PlayerPrefs.SetFloat("brillo", valor);
        PlayerPrefs.Save(); // Opcional pero recomendable
    }

    public void CambiarPantallaCompleta (bool esCompleta)
    {
        Screen.fullScreen = esCompleta;
        PlayerPrefs.SetInt("pantallaCompleta", esCompleta ? 1 : 0);
    }
}
