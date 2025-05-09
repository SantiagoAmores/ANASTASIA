using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnemigos : MonoBehaviour
{
    public int enemigoVida;
    public int enemigoAtaque;
    public float enemigoVelocidad;
    public int enemigoExperiencia;

    public bool esUnJefe;
    public int faseDeJefe;

    // Guarda las estadisticas de los enemigos en un dicciconario accesible
    public Dictionary<string, (int, int, float, int)> diccionarioEnemigos;
    public Dictionary<string, ((int, int, float, int) fase1, (int, int, float, int) fase2, string drop)> diccionarioBosses;

    private void Awake()
    {
        diccionarioEnemigos = new Dictionary<string, (int, int, float, int)>
        {
            { "Enemigo 1", (4, 1, 3f, 2) },
            { "Enemigo 2", (15, 2, 2.5f, 3) },
            { "Enemigo 4", (4, 1, 3f, 2) },
            { "Enemigo 5", (15, 2, 2.5f, 3) },
            { "Enemigo 7", (4, 1, 3f, 2) },
            { "Enemigo 8", (15, 2, 2.5f, 3) },
            { "Jarron", (1, 0, 0.1f, 0) }
        };

        diccionarioBosses = new Dictionary<string, ((int, int, float, int) fase1, (int, int, float, int) fase2, string drop)>
        {
            { "Enemigo 3", ((50, 2, 3.25f, 10), (200, 3, 3.25f, 0), "drop") },
            { "Enemigo 6", ((55, 3, 3.25f, 10), (220, 4, 3.25f, 0), "drop") },
            { "Enemigo 9", ((60, 4, 7f, 10), (240, 7, 20f, 0), "drop") },
        };
    }

    // Dependiendo del nombre del prefab de enemigo instanciado le asigna las correspondientes estadisticas
    public void revisarEnemigo()
    {
        string enemigoNombre = gameObject.name.Replace("(Clone)", "").Trim();
        if (diccionarioEnemigos.TryGetValue(enemigoNombre, out var estadisticasEnemigo))
        {
            esUnJefe = false;
            AsignarEstadisticas(estadisticasEnemigo);
        }
        else if (diccionarioBosses.TryGetValue(enemigoNombre, out var estadisticasJefe))
        {
            esUnJefe = true;
            if (faseDeJefe == 1)
            {
                AsignarEstadisticas(estadisticasJefe.fase1);
            }
            else if (faseDeJefe == 2)
            {
                AsignarEstadisticas(estadisticasJefe.fase2);
            }
        }
    }

    private void AsignarEstadisticas((int vida, int ataque, float velocidad, int experiencia) stats)
    {
        enemigoVida = stats.vida;
        enemigoAtaque = stats.ataque;
        enemigoVelocidad = stats.velocidad;
        enemigoExperiencia = stats.experiencia;
    }
}