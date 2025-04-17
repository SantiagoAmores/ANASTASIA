using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsAnastasia : MonoBehaviour
{
    // VIDA BASE DE ANASTASIA

    // VELOCIDAD BASE DE ANASTASIA
    public float velocidadMovimientoBase = 5f;

    // ARMA 1 BASE
    public float arma1CadenciaBase = 1.3f;
    public int arma1AtaqueBase = 1;

    // ARMA 2 BASE
    // cadencia base aqui
    // ataque base

    // ARMA 3 BASE
    // cadencia base aqui
    // ataque base

    // ARMA 4 BASE
    // cadencia base aqui
    // ataque base

    // ARMA 5 BASE
    // cadencia base aqui
    // ataque base

    // ARMA 6 BASE
    // cadencia base aqui
    // ataque base

    // Los getters y setters hacen que solo este script pueda modificar los stats con AumentarEstadisticas()
    public float velocidadMovimiento { get; private set; }
    public float arma1Cadencia {  get; private set; }
    public float arma1Ataque { get; private set; }

    // PONER EL RESTO DE LOS GETTERS Y SETTERS AQUI

    private void Start()
    {
        // ESTABLECER stats = statsBase
        velocidadMovimiento = velocidadMovimientoBase;
        
        arma1Cadencia = arma1CadenciaBase;
        arma1Ataque = arma1AtaqueBase;

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


        // Velocidad de movimiento
        velocidadMovimiento += 0.25f;

        // Cadencia de disparo
        arma1Cadencia = Mathf.Max(0.3f, arma1Cadencia - 0.1f);
        
        // Daño de las armas
        arma1Ataque += 1;
    }
}
