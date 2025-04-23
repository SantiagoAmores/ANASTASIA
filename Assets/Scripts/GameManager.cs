using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Estadisticas
    public int experienciaTotal = 0;
    public int nivel = 1;

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

        if (experienciaTotal % 5 == 0)
        {
            nivel++;
            if (anastasia != null)
            {
                anastasia.GetComponent<StatsAnastasia>().SubidaDeNivelAleatoria();
            }
        }
    }

    void ReiniciarNiveles()
    {
        experienciaTotal = 0;
        nivel = 1;
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