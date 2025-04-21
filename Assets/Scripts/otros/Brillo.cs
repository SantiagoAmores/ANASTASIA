using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Image panelOscuridad;

    void Start()
    {
        float brillo = PlayerPrefs.GetFloat("brillo", 1f); // Valor por defecto: 1 (sin oscuridad)
        Color c = panelOscuridad.color;
        c.a = 1f - brillo;
        panelOscuridad.color = c;
    }
}
