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

    [Header("Configuraci�n de Niveles")]
    public DesbloqueosNivel[] desbloqueosPorNivel;

    public void NivelCompletado(int nivelCompletado)
    {
        foreach (var desbloqueo in desbloqueosPorNivel)
        {
            if (desbloqueo.numeroNivel == nivelCompletado)
            {
                foreach (int indice in desbloqueo.indicesEntradas)
                {
                    // Guarda el �ndice tal cual (sin restar 1)
                    string clave = $"desbloqueo_{desbloqueo.categoria}_{indice}";
                    PlayerPrefs.SetInt(clave, 1);
                    Debug.Log($"Guardado: {clave}"); // Para depuraci�n
                }
            }
        }
        PlayerPrefs.Save();
    }

    // M�todo para verificar si una entrada est� desbloqueada
    public static bool EstaDesbloqueado(string categoria, int indice)
    {
        string clave = $"desbloqueo_{categoria}_{indice}";
        return PlayerPrefs.GetInt(clave, 0) == 1;
    }
}
