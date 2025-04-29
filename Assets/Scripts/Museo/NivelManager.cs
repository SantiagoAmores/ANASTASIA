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
                    // Guardar cada desbloqueo en PlayerPrefs
                    string clave = $"desbloqueo_{desbloqueo.categoria}_{indice}";
                    PlayerPrefs.SetInt(clave, 1);
                }
            }
        }
        PlayerPrefs.Save(); // Asegurar que se guarda inmediatamente
    }

    // Método para verificar si una entrada está desbloqueada
    public static bool EstaDesbloqueado(string categoria, int indice)
    {
        string clave = $"desbloqueo_{categoria}_{indice}";
        return PlayerPrefs.GetInt(clave, 0) == 1;
    }
}
