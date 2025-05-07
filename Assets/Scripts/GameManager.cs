using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Estadisticas
    public int experienciaTotal = 0;
    public int experienciaActual = 0;
    public int nivel = 1;
    public int experienciaRequerida = 5;

    public static GameManager instancia;
    public GameObject anastasia;
    public Enemigo jefeActual;

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
        string clave = "desbloqueo_niveles_1";
        if (!PlayerPrefs.HasKey(clave))
        {
            PlayerPrefs.SetInt(clave, 1);
            PlayerPrefs.Save();
        }

        ReiniciarNiveles();
        anastasia = GameObject.FindWithTag("Player");
        if (anastasia != null) { }
        else { //Debug.Log("En esta escena no esta Anastasia.");
             }
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
                anastasia.GetComponent<StatsAnastasia>().SubidaDeNivelAleatoria();
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