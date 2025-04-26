using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestiarioManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject bestiarioCanvas;
    public Transform contenidoBestiario;
    public GameObject entradaPrefab;

    [Header("Datos")]
    public StatsEnemigos statsEnemigos;
    public StatsAnastasia statsAnastasia;

    private bool abierto = false;

    public void ToggleBestiario()
    {
        abierto = !abierto;
        bestiarioCanvas.SetActive(abierto);

        if (abierto)
        {
            ActualizarBestiario();
        }
    }

    void ActualizarBestiario()
    {
        /*// Elimina entradas anteriores
        foreach (Transform child in contenidoBestiario)
        {
            Destroy(child.gameObject);
        }

        // ENEMIGOS
        foreach (var enemigo in statsEnemigos.diccionarioEnemigos)
        {
            GameObject nuevaEntrada = Instantiate(entradaPrefab, contenidoBestiario);
            nuevaEntrada.GetComponentInChildren<TextMeshProUGUI>().text = enemigo.Key + "\nVida: " + enemigo.Value.Item1 + "\nAtaque: " + enemigo.Value.Item2;
            // Aquí luego pondríamos la imagen también
        }

        // BOSS
        foreach (var boss in statsEnemigos.diccionarioBosses)
        {
            GameObject nuevaEntrada = Instantiate(entradaPrefab, contenidoBestiario);
            nuevaEntrada.GetComponentInChildren<TextMeshProUGUI>().text = boss.Key + " (Boss)\nVida Fase 1: " + boss.Value.fase1.Item1 + "\nVida Fase 2: " + boss.Value.fase2.Item1;
            // También luego pondríamos imagen y drop
        }

        // ARMAS
        GameObject entradaArmas = Instantiate(entradaPrefab, contenidoBestiario);
        entradaArmas.GetComponentInChildren<TextMeshProUGUI>().text =
            "Armas:\n" +
            "- Arma 1: Daño " + StatsAnastasia.arma1AtaqueBase + ", Cadencia " + StatsAnastasia.arma1CadenciaBase + "\n" +
            "- Arma 2: Daño " + StatsAnastasia.arma2AtaqueBase + ", Cadencia " + StatsAnastasia.arma2CadenciaBase + "\n" +
            "- ...";*/
    }
}
