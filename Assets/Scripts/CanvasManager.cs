using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    //GameManager
    GameManager gameManager;

    //Paneles Niveles
    public GameObject PanelVictoria;
    public GameObject PanelDerrota;
    public GameObject PanelOpciones;
    public GameObject PanelMuseo;

    //Estadisticas
    private TextMeshProUGUI textoExperiencia;
    private TextMeshProUGUI textoNivel;

    // Slider de Experiencia
    public Slider sliderExp;
    public int expMaxPorNivel = 5;


    //Cuenta atras
    public TextMeshProUGUI cuentaAtras;
    public float startTime = 120f;
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        textoExperiencia = GameObject.Find("TextoExperiencia").GetComponent<TextMeshProUGUI>();
        textoNivel = GameObject.Find("TextoNivel").GetComponent<TextMeshProUGUI>();

        sliderExp = GameObject.Find("SliderXP").GetComponent<Slider>();

        sliderExp.minValue = 0;
        sliderExp.maxValue = expMaxPorNivel;
        sliderExp.value = 0; // Inicia vacío

        timeLeft = startTime;
        StartCoroutine(Countdown());

        PanelDerrota.SetActive(false);
        PanelVictoria.SetActive(false);
        PanelOpciones.SetActive(false);
        PanelMuseo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        textoExperiencia.text = "Exp: " + gameManager.experienciaTotal.ToString();
        textoNivel.text = "Nv: " + gameManager.nivel.ToString();

        // Actualizar el Slider con la experiencia
        sliderExp.value = gameManager.experienciaTotal % expMaxPorNivel;
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            cuentaAtras.text = timeLeft.ToString("0"); 
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        Victoria();
        //cuentaAtras.text = "Te has quedado sin tiempo :(";
    }

    public void Victoria()
    {
        PanelVictoria.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    public void Derrota()
    {
        PanelDerrota.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    public void MenúInicio()
    {
        // Reanudar el tiempo antes de reiniciar
        Time.timeScale = 1f;

        //Volver al menu de inicio
        SceneManager.LoadScene("MenuInicio");
    }

    public void ReinicioNivel()
    {
        // Reanudar el tiempo antes de reiniciar
        Time.timeScale = 1f;

        // Cargar la escena desde el principio
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Opciones()
    {
        PanelOpciones.SetActive(true);
    }

    public void Atras()
    {
        PanelOpciones.SetActive(false);
        PanelMuseo.SetActive(false);
    }

    public void Museo()
    {
        PanelMuseo.SetActive(true);
    }
}
