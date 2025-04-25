using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsAnastasia : MonoBehaviour
{
    // VIDA BASE DE ANASTASIA
    public int vidaBase = 5;

    // VELOCIDAD BASE DE ANASTASIA
    public static float velocidadMovimientoBase = 5f;

    // ARMA 1 BASE
    public static float arma1CadenciaBase = 1.5f;
    public static int arma1AtaqueBase = 2;

    // ARMA 2 BASE
    public static float arma2CadenciaBase = 4f;
    public static int arma2AtaqueBase = 3;

    // ARMA 3 BASE
    public static float arma3CadenciaBase = 1f;
    public static int arma3AtaqueBase = 1;

    // ARMA 4 BASE
    public static float arma4CadenciaBase = 2f;
    public static int arma4AtaqueBase = 1;

    // ARMA 5 BASE
    public static float arma5CadenciaBase = 2f;
    public static int arma5AtaqueBase = 1;

    // ARMA 6 BASE
    public static float arma6CadenciaBase = 2f;
    public static int arma6AtaqueBase = 1;

    // LIMITAR SUBIDAS DE NIVEL
    public int mejorasVida = 0;
    public int mejorasVelocidad = 0;
    public int mejorasCadencia = 0;
    public int mejorasAtaque = 0;
    private const int mejorasMaximas = 5;

    public GameObject textoSubirNivelPrefab;

    // Los getters y setters hacen que solo este script pueda modificar los stats con AumentarEstadisticas()
    public int vida { get; private set; }
    public float velocidadMovimiento { get; private set; }
    public float arma1Cadencia {  get; private set; }
    public float arma2Cadencia { get; private set; }
    public float arma3Cadencia { get; private set; }
    public float arma4Cadencia { get; private set; }
    public float arma5Cadencia { get; private set; }
    public float arma6Cadencia { get; private set; }
    public float arma1Ataque { get; private set; }
    public float arma2Ataque { get; private set; }
    public float arma3Ataque { get; private set; }
    public float arma4Ataque { get; private set; }
    public float arma5Ataque { get; private set; }
    public float arma6Ataque { get; private set; }

    private void Start()
    {
        // ESTABLECER stats = statsBase
        vida = vidaBase;
        velocidadMovimiento = velocidadMovimientoBase;
        
        arma1Cadencia = arma1CadenciaBase;
        arma1Ataque = arma1AtaqueBase;

        arma2Cadencia = arma2CadenciaBase;
        arma2Ataque = arma2AtaqueBase;

        arma3Cadencia = arma3CadenciaBase;
        arma3Ataque = arma3AtaqueBase;

        arma4Cadencia = arma4CadenciaBase;
        arma4Ataque = arma4AtaqueBase;

        arma5Cadencia = arma5CadenciaBase;
        arma5Ataque = arma5AtaqueBase;

        arma6Cadencia = arma6CadenciaBase;
        arma6Ataque = arma6AtaqueBase;
    }

    public void SubidaDeNivelAleatoria()
    {
        List<int> subidasDeNivelIncompletas = new List<int>();

        if (mejorasVida < mejorasMaximas)
        {
            subidasDeNivelIncompletas.Add(0);
        }
        if (mejorasVelocidad < mejorasMaximas)
        { 
            subidasDeNivelIncompletas.Add(1);
        }
        if (mejorasCadencia < mejorasMaximas)
        { 
            subidasDeNivelIncompletas.Add(2);
            subidasDeNivelIncompletas.Add(2);
        }
        if (mejorasAtaque < mejorasMaximas)
        {
            subidasDeNivelIncompletas.Add(3);
            subidasDeNivelIncompletas.Add(3);
            subidasDeNivelIncompletas.Add(3);
        }

        if (subidasDeNivelIncompletas.Count == 0)
        {
            Debug.Log("Si sube al nivel maximo, le cura 5 de vida cada vez que sube de nivel!");
            return;
        }

        int subidaDeNivel = subidasDeNivelIncompletas[Random.Range(0, subidasDeNivelIncompletas.Count)];

        switch (subidaDeNivel)
        {
            case 0:
                AumentarVida();
                mejorasVida++;
                MostrarSubidaDeNivel("+ VIDA");
                break;
            case 1:
                AumentarVelocidadMovimiento();
                mejorasVelocidad++;
                MostrarSubidaDeNivel("+ VELOCIDAD");
                break;
            case 2:
                AumentarCadencia();
                mejorasCadencia++;
                MostrarSubidaDeNivel("+ CADENCIA");
                break;
            case 3:
                AumentarAtaque();
                mejorasAtaque++;
                MostrarSubidaDeNivel("+ ATAQUE");
                break;
        }
    }

    void AumentarVida()
    {
        // Vida
        vida += 3;
    }

    void AumentarVelocidadMovimiento()
    {
        // Velocidad de movimiento
        velocidadMovimiento += 0.25f;
    }

    void AumentarCadencia()
    {
        // Cadencia de disparos de las armas
        arma1Cadencia = Mathf.Max(0.5f, arma1Cadencia - 0.2f);
        arma2Cadencia = Mathf.Max(2.5f, arma2Cadencia - 0.3f);
        arma3Cadencia = Mathf.Max(0.3f, arma3Cadencia - 0.1f);
        arma4Cadencia = Mathf.Max(0.3f, arma4Cadencia - 0.1f);
        arma5Cadencia = Mathf.Max(0.3f, arma5Cadencia - 0.1f);
        arma6Cadencia = Mathf.Max(0.3f, arma6Cadencia - 0.1f);
    }

    void AumentarAtaque()
    {
        // Daño de las armas
        arma1Ataque += 1;
        arma2Ataque += 1;
        arma3Ataque += 1;
        arma4Ataque += 1;
        arma5Ataque += 1;
        arma6Ataque += 1;
    }

    void MostrarSubidaDeNivel(string subida)
    {
        if (textoSubirNivelPrefab != null)
        {
            float alturaOffset = 0.2f + (transform.localScale.y * 0.5f);
            Vector3 posicionTexto = transform.position + new Vector3(0, alturaOffset, 0);
            GameObject textoInstancia = Instantiate(textoSubirNivelPrefab, posicionTexto, Quaternion.identity);
            TextMeshProUGUI texto = textoInstancia.GetComponentInChildren<TextMeshProUGUI>();
            if (texto != null)
            {
                texto.text = subida.ToString();
            }
        }
    }
}
