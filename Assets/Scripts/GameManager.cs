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
        ReiniciarNiveles();
        anastasia = GameObject.FindWithTag("Player");
        if (anastasia != null)
        {

        }
        else
        {
            Debug.Log("En esta escena no esta Anastasia.");
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

            if (nivel % 5 == 0)
            {
                experienciaRequerida += 4;
            }

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