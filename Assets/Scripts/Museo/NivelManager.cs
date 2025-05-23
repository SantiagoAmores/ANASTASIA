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


    void Start()
    {
        RevisarColeccionableEnNivel(idNivel);
    }


    [Header("Coleccionables")]
    public GameObject prefabColeccionable; // Prefab del coleccionable
    public Transform[] puntosSpawnColeccionables; // Posiciones por nivel

    public void RevisarColeccionableEnNivel(int nivelActual)
    {
        int enemigosDerrotados = 0;

        if (GameManager.instancia.enemigosDerrotados.TryGetValue($"nivel_{nivelActual}", out enemigosDerrotados))
        {
            if (enemigosDerrotados >= 100)
            {
                string clave = $"desbloqueo_coleccionables_{nivelActual - 1}";
                if (PlayerPrefs.GetInt(clave, 0) == 0) // No recogido aún
                {
                    if (prefabColeccionable != null && puntosSpawnColeccionables.Length >= nivelActual)
                    {
                        var spawnPoint = puntosSpawnColeccionables[nivelActual - 1];
                        GameObject obj = Instantiate(prefabColeccionable, spawnPoint.position, Quaternion.identity);
                        obj.GetComponent<Coleccionable>().idColeccionable = nivelActual - 1;
                    }
                }
            }
        }
    }
}
