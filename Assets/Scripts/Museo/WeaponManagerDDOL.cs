using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManagerDDOL : MonoBehaviour
{
    public static WeaponManagerDDOL instancia;

    public int armaSeleccionada = -1;

    public static bool cargarEscena = false;

    public TextMeshProUGUI textoArma;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            armaSeleccionada = -1;
            SceneManager.sceneLoaded += activarArmaPorSiAcaso;

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= activarArmaPorSiAcaso;
    }

    void activarArmaPorSiAcaso(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Scene_Museo" && armaSeleccionada == -1)
        {
            SeleccionarArma(0);
        }
    }

    public void SeleccionarArma0() => SeleccionarArma(0);
    public void SeleccionarArma1() => SeleccionarArma(1);
    public void SeleccionarArma2() => SeleccionarArma(2);
    public void SeleccionarArma3() => SeleccionarArma(3);

    public void SeleccionarArma(int index)
    {
        if (textoArma == null)
        {
            textoArma = GameObject.Find("textoArma")?.GetComponent<TextMeshProUGUI>();
        }

        if (textoArma != null)
        {
            textoArma.text = "Arma numero " + index + " seleccionada.";
        }
        armaSeleccionada = index;
    }
}
