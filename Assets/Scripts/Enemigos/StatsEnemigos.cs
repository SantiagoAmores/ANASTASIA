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
    private Dictionary<string, (int, int, float, int)> diccionarioEnemigos;
    private Dictionary<string, ((int, int, float, int) fase1, (int, int, float, int) fase2, string drop)> diccionarioBosses;

    private void Awake()
    {
        diccionarioEnemigos = new Dictionary<string, (int, int, float, int)>
        {
            { "Enemigo 1", (4, 1, 2.75f, 1) },
            { "Enemigo 2", (6, 2, 3, 2) },
            { "Enemigo 4", (4, 2, 2.75f, 1) },
            { "Enemigo 5", (10, 4, 2.5f, 2) },
            { "Enemigo 7", (2, 1, 4.25f, 1) },
            { "Enemigo 8", (5, 4, 3, 2) }
        };

        diccionarioBosses = new Dictionary<string, ((int, int, float, int) fase1, (int, int, float, int) fase2, string drop)>
        {
            { "Enemigo 3", ((25, 2, 3.25f, 10), (40, 3, 3.25f, 0), "drop") },
            { "Enemigo 6", ((35, 3, 3.25f, 10), (50, 4, 3.25f, 0), "drop") },
            { "Enemigo 9", ((40, 4, 3.25f, 10), (55, 7, 3.5f, 0), "drop") },
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