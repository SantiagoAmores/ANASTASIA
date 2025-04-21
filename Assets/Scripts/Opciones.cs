using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Opciones : MonoBehaviour
{
    [Header("Brillo")]
    public Image panelBrillo;
    public Slider sliderBrillo;

    [Header("Pantalla")]
    public Toggle pantallaCompletaToggle;
    public TMPro.TMP_Dropdown dropdownResolucion;
    private Resolution[] resoluciones;
    private int resolucionActualIndex;

    [Header("Volumen")]
    public Slider sliderVolumen;
    public AudioMixer mixer;
    public GameObject[] notas;

    // Start is called before the first frame update
    void Start()
    {
        //Brillo
        float brilloGuardado = PlayerPrefs.GetFloat("Brillo", 1f);
        sliderBrillo.value = brilloGuardado;
        CambiarBrillo(brilloGuardado);

        //Pantalla completa
        bool pantallaCompleta = PlayerPrefs.GetInt("pantallaCompleta", 1) == 1;
        pantallaCompletaToggle.isOn = pantallaCompleta;
        CambiarPantallaCompleta(pantallaCompleta);

        //Cargar volumen guardado
        float volumenGuardado = PlayerPrefs.GetFloat("Volumen", 1f);
        sliderVolumen.value = volumenGuardado;
        CambiarVolumen(volumenGuardado);
        sliderVolumen.onValueChanged.AddListener(delegate { CambiarVolumen(sliderVolumen.value); });

        //Resoluciones
        resoluciones = Screen.resolutions;
        dropdownResolucion.ClearOptions();

        List<string> opciones = new List<string>();
        resolucionActualIndex = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
            opciones.Add(opcion);

            if (resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActualIndex = i;
            }
        }

        dropdownResolucion.AddOptions(opciones);
        dropdownResolucion.value = resolucionActualIndex;
        dropdownResolucion.RefreshShownValue();

        // Listener
        dropdownResolucion.onValueChanged.AddListener(CambiarResolucion);
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
        PlayerPrefs.SetFloat("Brillo", valor);
        PlayerPrefs.Save(); // Opcional pero recomendable
    }

    public void CambiarPantallaCompleta (bool esCompleta)
    {
        Screen.fullScreen = esCompleta;
        PlayerPrefs.SetInt("pantallaCompleta", esCompleta ? 1 : 0);
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolucionIndex", indiceResolucion);
    }

    public void CambiarVolumen(float valor)
    {
        valor = Mathf.Clamp(valor, 0.1f, 1f);

        if (valor < 0.1f)
        {
            mixer.SetFloat("Volumen general", -80f);
        }
        else
        {
            float volumenDB = Mathf.Log10(valor) * 20;
            mixer.SetFloat("Volumen general", volumenDB);

            PlayerPrefs.SetFloat("Volumen", valor);
            PlayerPrefs.Save();

            //mostrar las notas segun el volumen
            int cantidadNotas = Mathf.RoundToInt(valor * notas.Length);

            for (int i = 0; i < notas.Length; i++)
            {
                notas[i].SetActive(i < cantidadNotas);
            }
        }
    }
}
