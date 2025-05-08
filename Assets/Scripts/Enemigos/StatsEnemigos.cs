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
    public Dictionary<string, ((int, int, float, int) fase1, (int, int, float, int) fase2)> diccionarioBosses;

    private void Awake()
    {
        diccionarioEnemigos = new Dictionary<string, (int, int, float, int)>
        {
            { "Enemigo 1", (3, 1, 2.75f, 2) },
            { "Enemigo 2", (12, 3, 3f, 3) },
            { "Enemigo 4", (3, 1, 2.75f, 2) },
            { "Enemigo 5", (12, 3, 3f, 3) },
            { "Enemigo 7", (3, 1, 2.75f, 2) },
            { "Enemigo 8", (12, 3, 3f, 3) },
            { "Jarron", (1, 0, 0.1f, 0) }
        };

        diccionarioBosses = new Dictionary<string, ((int, int, float, int) fase1, (int, int, float, int) fase2)>
        {
            { "Enemigo 3", ((70, 2, 3.25f, 10), (250, 3, 4f, 0)) },
            { "Enemigo 6", ((75, 2, 3.25f, 10), (260, 6, 4f, 0)) },
            { "Enemigo 9", ((80, 1, 7f, 10), (270, 2, 20f, 0)) },
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