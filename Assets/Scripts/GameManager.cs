using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Instancia")]
    public static GameManager instancia;

    [Header("Referencias")]
    public GameObject anastasia;
    public Enemigo jefeActual;

    [Header("Estadisticas")]
    public int experienciaTotal = 0;
    public int experienciaActual = 0;
    public int nivel = 1;
    public int experienciaRequerida = 5;

    private const string desbloqueo_base = "desbloqueo_niveles_1";
    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += AlCargarEscena;
        }
        else if (instancia != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instancia == this)
        {
            SceneManager.sceneLoaded -= AlCargarEscena;
        }
    }

    void AlCargarEscena(Scene scene, LoadSceneMode mode)
    {
        DesbloqueoInicialCheck();
        ReiniciarNiveles();
        BuscarAnastasia();
    }

    public void SubirNivel()
    {
        experienciaTotal++;
        experienciaActual++;

        if (experienciaActual >= experienciaRequerida)
        {
            nivel++;
            experienciaActual = 0;
            experienciaRequerida++;

            if (anastasia != null)
            {
                var stats = anastasia.GetComponent<StatsAnastasia>();
                stats?.SubidaDeNivelAleatoria();
            }
        }
    }

    void ReiniciarNiveles()
    {
        experienciaTotal = 0;
        experienciaActual = 0;
        nivel = 1;
        experienciaRequerida = 5;
    }

    void DesbloqueoInicialCheck()
    {
        if (!PlayerPrefs.HasKey(desbloqueo_base))
        {
            PlayerPrefs.SetInt(desbloqueo_base, 1);
            PlayerPrefs.Save();
        }
    }

    void BuscarAnastasia()
    {
        anastasia = GameObject.FindWithTag("Player");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void CrearGameManagerAutomaticamente()
    {
        if (instancia == null)
        {
            GameObject gm = new GameObject("GameManager");
            gm.AddComponent<GameManager>();
        }
    }
}