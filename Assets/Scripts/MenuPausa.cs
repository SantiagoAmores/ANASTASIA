using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;

    //variables para comprobar si hay otros paneles abiertos y asi que no te deje pausar
    public GameObject panelVictoria;
    public GameObject panelDerrota;

    public GameObject tieneEstadisticas;
    public TextMeshProUGUI estadisticasTexto;
    public StatsAnastasia estadisticasScript;
    public GameManager gameManager;
    public MovimientoJugador movimientoJugador;

    void Start()
    {
        estadisticasScript = FindAnyObjectByType<StatsAnastasia>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        movimientoJugador = GameObject.Find("Anastasia").GetComponent<MovimientoJugador>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) 
            && !panelVictoria.activeSelf 
            && !panelDerrota.activeSelf
            && movimientoJugador.anastasiaViva)
        {
            if (menuPausa.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        menuPausa.SetActive(true);
        if (PlayerPrefs.GetInt("LocaleKey") == 0)
        {
            estadisticasTexto.text =    "ESTADÍSTIQUES ACTUALS\nSALUT: " + estadisticasScript.mejorasVida +
                                        "\nATAC: " + estadisticasScript.mejorasAtaque +
                                        "\nCADÈNCIA: " + estadisticasScript.mejorasCadencia +
                                        "\nVELOCITAT: " + estadisticasScript.mejorasVelocidad +
                                        "\nENEMICS DERROTATS: " + gameManager.contadorEnemigosDerrotados;
        }
        else if (PlayerPrefs.GetInt("LocaleKey") == 1)
        {
            estadisticasTexto.text =    "CURRENT STATS\nHEALTH: " + estadisticasScript.mejorasVida +
                                        "\nATTACK: " + estadisticasScript.mejorasAtaque +
                                        "\nCADENCE: " + estadisticasScript.mejorasCadencia +
                                        "\nSPEED: " + estadisticasScript.mejorasVelocidad +
                                        "\nDEFEATED ENEMIES: " + gameManager.contadorEnemigosDerrotados;
        }
        else if (PlayerPrefs.GetInt("LocaleKey") == 2)
        {
            estadisticasTexto.text =    "ESTADÍSTICAS ACTUALES\nVIDA: " + estadisticasScript.mejorasVida +
                                        "\nATAQUE: " + estadisticasScript.mejorasAtaque +
                                        "\nCADENCIA: " + estadisticasScript.mejorasCadencia +
                                        "\nVELOCIDAD: " + estadisticasScript.mejorasVelocidad +
                                        "\nENEMIGOS DERROTADOS: " + gameManager.contadorEnemigosDerrotados;
        }

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
    }
}