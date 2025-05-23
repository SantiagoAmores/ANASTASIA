using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class CanvasManager : MonoBehaviour
{
    // GameManager
    private GameManager gameManager;

    [Header("Paneles UI")]
    public GameObject PanelVictoria;
    public GameObject PanelDerrota;
    public GameObject PanelOpciones;
    public GameObject PanelReinicio;
    public GameObject PanelPausa;

    [Header("Textos")]
    public TextMeshProUGUI cuentaAtras;
    public TextMeshProUGUI textoNivel;
    public TextMeshProUGUI textoRonda;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoExperiencia;

    [Header("Sliders")]
    public Slider sliderExp;
    public Slider sliderVida;
    public GameObject sliderBossObject;
    public Slider sliderBoss;

    [Header("Parámetros del jugador")]
    public MovimientoJugador statVida;
    public StatsAnastasia estadisticasScript;
    public TextMeshProUGUI estadisticasTexto;

    [Header("Cuenta atrás")]
    public float startTime = 120f;
    private float timeLeft;

    [Header("Objeto activable")]
    public GameObject objetoActivable;
    public GameObject objeto1;
    public GameObject objeto2;
    public GameObject objeto3;
    public GameObject objeto4;
    public GameObject objeto5;

    [Header("Cámara cinemática final")]
    public CinemachineVirtualCamera camaraFinal;

    [Header("Animaciones")]
    public Animator animJugador;
    public string animIdle = "Idle";
    public string animWalk = "Walk";
    public string animVictoria = "Victoria";
    public string animDerrota = "Derrota";

    void Start()
    {
        InicializarReferencias();
        ConfigurarSliders();
        OcultarPanelesIniciales();
        camaraFinal.gameObject.SetActive(false);
        estadisticasScript = FindAnyObjectByType<StatsAnastasia>();
        estadisticasTexto.gameObject.SetActive(false);

        if (animJugador == null)
        {
            GameObject jugador = GameObject.FindWithTag("Player");
            if (jugador != null)
            {
                animJugador = jugador.GetComponentInChildren<Animator>();
            }
        }
    }

    void Update()
    {
        ActualizarInterfaz();
    }

    void InicializarReferencias()
    {
        gameManager = GameObject.Find("GameManager")?.GetComponent<GameManager>();
        statVida = GameObject.FindWithTag("Player")?.GetComponent<MovimientoJugador>();

        if (!textoNivel) textoNivel = GameObject.Find("TextoNivel")?.GetComponent<TextMeshProUGUI>();
        if (!textoRonda) textoRonda = GameObject.Find("TextoRonda")?.GetComponent<TextMeshProUGUI>();
        if (!textoVida) textoVida = GameObject.Find("TextoVida")?.GetComponent<TextMeshProUGUI>();
        if (!textoExperiencia) textoExperiencia = GameObject.Find("TextoExperiencia")?.GetComponent<TextMeshProUGUI>();
        if (!sliderExp) sliderExp = GameObject.Find("SliderXP")?.GetComponent<Slider>();
        if (!sliderVida) sliderVida = GameObject.Find("SliderVida")?.GetComponent<Slider>();
        if (!sliderBossObject) sliderBossObject = GameObject.Find("BarraBoss");
        if (!sliderBoss) sliderBoss = GameObject.Find("SliderBoss")?.GetComponent<Slider>();

        if (!objetoActivable) objetoActivable = GameObject.Find("Activable");
        if (!objeto1) objeto1 = GameObject.Find("Objeto1");
        if (!objeto2) objeto2 = GameObject.Find("Objeto2");
        if (!objeto3) objeto3 = GameObject.Find("Objeto3");
        if (!objeto4) objeto4 = GameObject.Find("Objeto4");
        if (!objeto5) objeto5 = GameObject.Find("Objeto5");

        if (PlayerPrefs.GetInt("textoVidaExp") == 0)
        {
            textoVida.gameObject.SetActive(false);
            textoExperiencia.gameObject.SetActive(false);
        }
    }

    void ConfigurarSliders()
    {
        if (statVida != null)
        {
            sliderVida.minValue = 0;
            sliderVida.maxValue = statVida.vidaTotal;
            sliderVida.value = statVida.vidaTotal;
        }

        if (gameManager != null)
        {
            sliderExp.minValue = 0;
            sliderExp.maxValue = gameManager.experienciaRequerida;
            sliderExp.value = gameManager.experienciaActual;
        }
    }

    void OcultarPanelesIniciales()
    {
        PanelDerrota?.SetActive(false);
        PanelVictoria?.SetActive(false);
        PanelOpciones?.SetActive(false);
        objetoActivable?.SetActive(false);
        sliderBossObject?.SetActive(false);
        sliderBoss?.gameObject.SetActive(false);
        objeto1?.SetActive(false);
        objeto2?.SetActive(false);
        objeto3?.SetActive(false);
        objeto4?.SetActive(false);
        objeto5?.SetActive(false);

    }

    void ActualizarInterfaz()
    {
        if (gameManager != null)
        {
            //textoExperiencia.text = "Exp: " + gameManager.experienciaTotal.ToString();
            textoNivel.text = "Nv: " + gameManager.nivel.ToString();

            // Actualizar el Slider con la experiencia
            sliderExp.maxValue = gameManager.experienciaRequerida;
            sliderExp.value = gameManager.experienciaActual;
        }

        if (statVida != null)
        {
            // Actualizar el slider de la vida que tiene
            sliderVida.maxValue = statVida.vidaTotal;
            sliderVida.value = statVida.vidaActual;
        }

        if (sliderBossObject.activeSelf && gameManager.jefeActual != null)
        {
            sliderBoss.maxValue = gameManager.jefeActual.enemigoVidaTotal;
            sliderBoss.value = gameManager.jefeActual.enemigoVidaActual;
        }

        if (textoVida != null && textoVida.isActiveAndEnabled)
        {
            textoVida.text = statVida.vidaActual.ToString() + " / " + statVida.vidaTotal.ToString();
        }

        if (textoExperiencia != null && textoExperiencia.isActiveAndEnabled)
        {
            textoExperiencia.text = gameManager.experienciaActual.ToString() + " / " + gameManager.experienciaRequerida.ToString();
        }
    }

    IEnumerator Countdown()
    {
        while (timeLeft > 0)
        {
            cuentaAtras.text = timeLeft.ToString("0"); 
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }
    }

    public void Victoria()
    {
        StartCoroutine(SecuenciaFinal(true));
    }

    public void Derrota()
    {
        StartCoroutine(SecuenciaFinal(false));
    }

    IEnumerator SecuenciaFinal(bool esVictoria)
    {

        // Buscar y destruir a todos los enemigos
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemigo in enemigos)
        {
            Destroy(enemigo);
        }

        if (camaraFinal != null)
        {
            camaraFinal.gameObject.SetActive(true);

        }

        estadisticasTexto.text = "ESTADÍSTICAS ACTUALES\nVIDA: " + estadisticasScript.mejorasVida +
                                    "\nATAQUE: " + estadisticasScript.mejorasAtaque +
                                    "\nCADENCIA: " + estadisticasScript.mejorasCadencia +
                                    "\nVELOCIDAD: " + estadisticasScript.mejorasVelocidad +
                                    "\nENEMIGOS DERROTADOS: " + gameManager.contadorEnemigosDerrotados;

        yield return new WaitForSeconds(0.5f);

        // Esperar un frame para asegurar que la cámara se active correctamente
        yield return null;

        if (animJugador != null)
        {
            if (esVictoria)
            {
                animJugador.Play(animVictoria);
                animJugador.Play("Expresion_Victoria", layer: 1); // Layer 1 si es tu capa facial
            }
            else
            {
                animJugador.Play(animDerrota);
                animJugador.Play("Expresion_Derrota", layer: 1);
            }
        }

        yield return new WaitForSeconds(1.5f);

        // Desactivamos todo el Canvas antes de mostrar los paneles
        DesactivarElementosCanvas();

        if (esVictoria)
        {
            if (PanelVictoria != null)
            {
                PanelVictoria.SetActive(true);
            }
        }
        else
        {
            if (PanelDerrota != null)
            {
                PanelDerrota.SetActive(true);
            }
        }

        estadisticasTexto.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    void DesactivarElementosCanvas()
    {
        // Desactiva todos los hijos activos del Canvas, excepto los paneles de victoria/derrota
        foreach (Transform hijo in transform)
        {
            if (hijo.gameObject.activeSelf &&
                hijo.gameObject != PanelVictoria &&
                hijo.gameObject != PanelDerrota)
            {
                hijo.gameObject.SetActive(false);
            }
        }
    }

    public void MenúInicio()
    {
        // Reanudar el tiempo antes de reiniciar
        Time.timeScale = 1f;

        //Volver al menu de inicio
        SceneManager.LoadScene("MenuInicio");
    }

    public void MenuReinicio()
    {
        PanelPausa.SetActive(false);
        PanelReinicio.SetActive(true);
    }

    public void VolverAlNivel()
    {
        PanelReinicio.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ReinicioNivel()
    {
        // Reanudar el tiempo antes de reiniciar
        Time.timeScale = 1f;

        // Cargar la escena desde el principio
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Museo()
    {
        SceneManager.LoadScene("Scene_Museo");
    }

    public void ActualizarTextoRonda(string texto)
    {
        if (textoRonda != null)
        {
            textoRonda.text = texto;
        }
    }

    public void InfinitoCuentaAtras()
    {
        StopAllCoroutines();
        cuentaAtras.text = "";
    }

    public void ReiniciarCuentaAtras()
    {
        StopAllCoroutines();
        timeLeft = startTime;
        StartCoroutine(Countdown());
    }
}