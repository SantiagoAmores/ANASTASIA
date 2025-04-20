using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsAnastasia : MonoBehaviour
{
    // VIDA BASE DE ANASTASIA
    public int vidaBase = 5;

    // VELOCIDAD BASE DE ANASTASIA
    public static float velocidadMovimientoBase = 5f;

    // ARMA 1 BASE
    public static float arma1CadenciaBase = 1.3f;
    public static int arma1AtaqueBase = 1;

    // ARMA 2 BASE
    public static float arma2CadenciaBase = 3f;
    public static int arma2AtaqueBase = 2;

    // ARMA 3 BASE
    public static float arma3CadenciaBase = 1f;
    public static int arma3AtaqueBase = 1;

    // ARMA 4 BASE
    public static float arma4CadenciaBase = 2f;
    public static int arma4AtaqueBase = 1;

    // ARMA 5 BASE
    // cadencia base aqui
    // ataque base

    // ARMA 6 BASE
    public static float arma6CadenciaBase = 2f;
    public static int arma6AtaqueBase = 1;

    // Los getters y setters hacen que solo este script pueda modificar los stats con AumentarEstadisticas()

    public int vida { get; private set; }
    public float velocidadMovimiento { get; private set; }
    public float arma1Cadencia {  get; private set; }
    public float arma2Cadencia { get; private set; }
    public float arma3Cadencia { get; private set; }
    public float arma4Cadencia { get; private set; }

    //arma 5 

    public float arma6Cadencia { get; private set; }



    public float arma1Ataque { get; private set; }
    public float arma2Ataque { get; private set; }
    public float arma3Ataque { get; private set; }
    public float arma4Ataque { get; private set; }

    //arma 5

    public float arma6Ataque { get; private set; }



    // PONER EL RESTO DE LOS GETTERS Y SETTERS AQUI

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

        //arma 5

        arma6Cadencia = arma6CadenciaBase;
        arma6Ataque = arma6AtaqueBase;

        // Llama a AumentarEstadisticas al subir de nivel
        GameManager.AlSubirDeNivel += AumentarEstadisticas;
    }

    // Esto es para evitar errores
    private void OnDestroy()
    {
        GameManager.AlSubirDeNivel -= AumentarEstadisticas;
    }

    void AumentarEstadisticas()
    {
        // Vida
        vida += 1;

        // Velocidad de movimiento
        velocidadMovimiento += 0.25f;

        // Cadencia de disparos de las armas
        arma1Cadencia = Mathf.Max(0.3f, arma1Cadencia - 0.1f);
        arma2Cadencia = Mathf.Max(1.5f, arma2Cadencia - 0.3f);
        arma3Cadencia = Mathf.Max(0.3f, arma3Cadencia - 0.1f);
        arma4Cadencia = Mathf.Max(0.3f, arma4Cadencia - 0.1f);
        //arma 5
        arma6Cadencia = Mathf.Max(0.3f, arma6Cadencia - 0.1f);


        // Daño de las armas
        arma1Ataque += 1;
        arma2Ataque += 1;
        arma3Ataque += 1;
        arma4Ataque += 1;
        //arma 5
        arma6Ataque += 1;
    }
}
