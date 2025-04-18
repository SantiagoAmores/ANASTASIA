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
            { "Enemigo 1", (2, 1, 3, 1) },
            { "Enemigo 2", (3, 1, 3, 2) },
            { "Enemigo 4", (2, 2, 3, 1) },
            { "Enemigo 5", (4, 3, 3, 2) },
            { "Enemigo 7", (3, 2, 3, 1) },
            { "Enemigo 8", (5, 4, 3, 2) }
        };

        diccionarioBosses = new Dictionary<string, ((int, int, float, int) fase1, (int, int, float, int) fase2, string drop)>
        {
            { "Enemigo 3", ((8, 2, 3.25f, 10), (15, 3, 3.25f, 0), "drop") },
            { "Enemigo 6", ((10, 3, 3.25f, 10), (20, 4, 3.25f, 0), "drop") },
            { "Enemigo 9", ((15, 4, 3.25f, 10), (50, 7, 3.5f, 0), "drop") },
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
            AsignarEstadisticas(estadisticasJefe.fase1);
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