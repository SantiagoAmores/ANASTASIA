using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void NivelCompletado(int nivelCompletado)
    {
        foreach (var desbloqueo in desbloqueosPorNivel)
        {
            if (desbloqueo.numeroNivel == nivelCompletado)
            {
                foreach (int indice in desbloqueo.indicesEntradas)
                {
                    // Guarda el índice tal cual (sin restar 1)
                    string clave = $"desbloqueo_{desbloqueo.categoria}_{indice}";
                    PlayerPrefs.SetInt(clave, 1);
                    Debug.Log($"Guardado: {clave}"); // Para depuración
                }
            }
        }
        PlayerPrefs.Save();
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
                coleccionable.GetComponent<Coleccionable>().idColeccionable = idColeccionable;
                Debug.Log($"¡Coleccionable {idColeccionable} aparecido en nivel {nivelActual}!");
            }
        }
    }
}
