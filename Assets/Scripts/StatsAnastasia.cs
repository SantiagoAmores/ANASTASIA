using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsAnastasia : MonoBehaviour
{
    // VIDA BASE DE ANASTASIA
    public int vidaBase = 10;

    // VELOCIDAD BASE DE ANASTASIA
    public static float velocidadMovimientoBase = 4.25f;

    // ARMA 1 BASE
    public static float arma1CadenciaBase = 1.6f;
    public static int arma1AtaqueBase = 2;

    // ARMA 2 BASE
    public static float arma2CadenciaBase = 4.5f;
    public static int arma2AtaqueBase = 3;

    // ARMA 3 BASE
    public static float arma3CadenciaBase = 6f;
    public static int arma3AtaqueBase = 1;

    // ARMA 4 BASE
    public static float arma4CadenciaBase = 1.8f;
    public static int arma4AtaqueBase = 1;

    // ARMA 5 BASE
    public static float arma5CadenciaBase = 2f;
    public static int arma5AtaqueBase = 1;

    // ARMA 6 BASE
    public static float arma6CadenciaBase = 3f;
    public static int arma6AtaqueBase = 1;

    // ARMAS DPS
    public static float ticsPorSegundoBase = 1f;

    // LIMITAR SUBIDAS DE NIVEL
    public int mejorasVida = 0;
    public int mejorasVelocidad = 0;
    public int mejorasCadencia = 0;
    public int mejorasAtaque = 0;
    private const int mejorasMaximas = 8;

    public GameObject textoSubirNivelPrefab;
    private int ultimoStatMejorado = -1;
    private MovimientoJugador movimientoJugador;

    // Los getters y setters hacen que solo este script pueda modificar los stats con AumentarEstadisticas()
    public int vida { get; private set; }
    public float velocidadMovimiento { get; private set; }
    public float arma1Cadencia {  get; private set; }
    public float arma2Cadencia { get; private set; }
    public float arma3Cadencia { get; private set; }
    public float arma4Cadencia { get; private set; }
    public float arma5Cadencia { get; private set; }
    public float arma6Cadencia { get; private set; }
    public float ticsPorSegundo { get; private set; }
    public float arma1Ataque { get; private set; }
    public float arma2Ataque { get; private set; }
    public float arma3Ataque { get; private set; }
    public float arma4Ataque { get; private set; }
    public float arma5Ataque { get; private set; }
    public static float arma6Ataque { get; private set; }

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

        ticsPorSegundo = ticsPorSegundoBase;

        movimientoJugador = GameObject.FindWithTag("Player").GetComponent<MovimientoJugador>() ;
    }

    public void SubidaDeNivelAleatoria()
    {
        List<int> subidasDeNivelIncompletas = new List<int>();

        if (mejorasVida < mejorasMaximas && ultimoStatMejorado != 0)
        {
            subidasDeNivelIncompletas.Add(0);
        }
        if (mejorasVelocidad < mejorasMaximas && ultimoStatMejorado != 1)
        { 
            subidasDeNivelIncompletas.Add(1);
        }
        if (mejorasCadencia < mejorasMaximas && ultimoStatMejorado != 2)
        { 
            subidasDeNivelIncompletas.Add(2);
            subidasDeNivelIncompletas.Add(2);
        }
        if (mejorasAtaque < mejorasMaximas && ultimoStatMejorado != 3)
        {
            subidasDeNivelIncompletas.Add(3);
            subidasDeNivelIncompletas.Add(3);
            subidasDeNivelIncompletas.Add(3);
        }

        if (subidasDeNivelIncompletas.Count == 0)
        {
            if (mejorasVida < mejorasMaximas) subidasDeNivelIncompletas.Add(0);
            if (mejorasVelocidad < mejorasMaximas) subidasDeNivelIncompletas.Add(1);
            if (mejorasCadencia < mejorasMaximas) subidasDeNivelIncompletas.Add(2);
            if (mejorasAtaque < mejorasMaximas) subidasDeNivelIncompletas.Add(3);
        }

        if (subidasDeNivelIncompletas.Count == 0)
        {
            movimientoJugador.Curar(10);
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

        ultimoStatMejorado = subidaDeNivel;
    }

    void AumentarVida()
    {
        // Vida
        vida += 5;
    }

    void AumentarVelocidadMovimiento()
    {
        // Velocidad de movimiento
        velocidadMovimiento += 0.33f;
    }

    void AumentarCadencia()
    {
        // Cadencia de disparos de las armas
        arma1Cadencia = Mathf.Max(0.7f, arma1Cadencia - 0.15f);
        arma2Cadencia = Mathf.Max(2.1f, arma2Cadencia - 0.3f);
        arma3Cadencia = Mathf.Max(2f, arma3Cadencia - 0.5f);
        arma1Cadencia = Mathf.Max(1f, arma4Cadencia - 0.1f);
        arma5Cadencia = Mathf.Max(0.3f, arma5Cadencia - 0.1f);
        arma6Cadencia = Mathf.Max(0.3f, arma6Cadencia - 0.1f);
        ticsPorSegundo = Mathf.Max(0.05f, ticsPorSegundo - 0.11875f);
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

    public IEnumerator aumentoAtaque()
    {
        Debug.Log("¡A pegar!");
        arma1Ataque += 3;
        arma2Ataque += 3;
        arma3Ataque += 3;
        arma4Ataque += 3;
        arma5Ataque += 3;
        arma6Ataque += 3;
        yield return new WaitForSeconds(5f);
        Debug.Log("¡Vamos a calmarnos!");
        arma1Ataque -= 3;
        arma2Ataque -= 3;
        arma3Ataque -= 3;
        arma4Ataque -= 3;
        arma5Ataque -= 3;
        arma6Ataque -= 3;
    }

}
