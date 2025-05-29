using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NivelManager : MonoBehaviour
{
    [System.Serializable]
    public class DesbloqueosNivel
    {
        public int numeroNivel;
        public string categoria;
        public int[] indicesEntradas; // Entradas a desbloquear en este nivel
    }

    [Header("Configuración de Niveles")]
    public DesbloqueosNivel[] desbloqueosPorNivel;

    [Header("Cinemática Final")]
    public GameObject cinematicaFinal;
    private const string CINEMATICA_FINAL_VISTA = "CinematicaFinalVista";

    public void NivelCompletado(int nivelCompletado)
    {
        foreach (var desbloqueo in desbloqueosPorNivel)
        {
            if (desbloqueo.numeroNivel == nivelCompletado)
            {
                foreach (int indice in desbloqueo.indicesEntradas)
                {
                    string clave = $"desbloqueo_{desbloqueo.categoria}_{indice}";
                    PlayerPrefs.SetInt(clave, 1);
                    Debug.Log($"Guardado: {clave}");
                }
            }
        }

        PlayerPrefs.Save();

        if (nivelCompletado == 3)
        {
            if (PlayerPrefs.GetInt(CINEMATICA_FINAL_VISTA, 0) == 0)
            {
                PlayerPrefs.SetInt(CINEMATICA_FINAL_VISTA, 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Scene_Cinematica_Final"); // <- asegúrate de que esta escena exista
            }
            else
            {
                SceneManager.LoadScene("Scene_Museo");
            }
        }
    }

    public void MostrarCinematicaFinal()
    {
        if (cinematicaFinal != null)
        {
            cinematicaFinal.SetActive(true);
            // Desactivar otros elementos del juego si es necesario
            Time.timeScale = 0f; // Pausar el juego durante la cinemática
        }
    }

    // Método para verificar si una entrada está desbloqueada
    public static bool EstaDesbloqueado(string categoria, int indice)
    {
        string clave = $"desbloqueo_{categoria}_{indice}";
        return PlayerPrefs.GetInt(clave, 0) == 1;
    }

    [Header("Nivel Actual")]
    public int idNivel = 1;

    [Header("Coleccionables")]
    public GameObject prefabColeccionable; // Prefab del coleccionable
    public Transform[] puntosSpawnColeccionables; // Posiciones por nivel
    public float intervaloVerificacion = 10f; // Cada 10 segundos


    void Start()
    {
        RevisarColeccionableEnNivel(idNivel);
        InvokeRepeating("VerificarColeccionablePeriodicamente", intervaloVerificacion, intervaloVerificacion);
    }

    void VerificarColeccionablePeriodicamente()
    {
        RevisarColeccionableEnNivel(idNivel);
    }

    public void RevisarColeccionableEnNivel(int nivelActual)
    {
        int idColeccionable = nivelActual - 1;
        string clave = $"desbloqueo_coleccionables_{idColeccionable}";

        // Si ya está desbloqueado, no hacer nada
        if (PlayerPrefs.GetInt(clave, 0) == 1) return;

        // Verificar contador
        string claveNivel = $"nivel_{nivelActual}";
        if (GameManager.instancia.enemigosDerrotados.TryGetValue(claveNivel, out int cantidad) && cantidad >= 100)
        {
            if (puntosSpawnColeccionables.Length > idColeccionable)
            {
                Vector3 posicionSpawn = puntosSpawnColeccionables[idColeccionable].position;
                GameObject coleccionable = Instantiate(prefabColeccionable, posicionSpawn, Quaternion.identity);
                Coleccionable componente = coleccionable.GetComponentInChildren<Coleccionable>();  
                if (componente != null)
                {
                    componente.idColeccionable = idColeccionable;
                    Debug.Log($"¡Coleccionable {idColeccionable} aparecido en nivel {nivelActual}!");

                }
                else
                {
                    Debug.LogWarning("no se ha encontrado");
                }
                /*coleccionable.GetComponent<Coleccionable>().idColeccionable = idColeccionable;*/
            }
        }
    }
}
