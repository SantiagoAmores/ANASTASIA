using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class PantallaIniciarNivel : MonoBehaviour
{
    public GameObject PantallaNivelCanvas;
    public string nivel;
    public Button empezarNivel;
    public TMP_Text textoNivel;
    public TMP_Text armaSeleccionada;

    // NOTA: Cada uno de los triggers incluye el nivel por escrito desde el inspector de Unity.
    // Elimina las funciones del boton de empezar el nivel al principio para evitar bugs, y cada vez que se entra en el collider de uno de los niveles
    // se vuelve a asignar la funcion del nivel teniendo en cuenta el nivel escrito por el inspector

    void Start()
    {
        PantallaNivelCanvas.SetActive(false);
        
        if (empezarNivel == null)
        {
            empezarNivel = PantallaNivelCanvas.GetComponentInChildren<Button>();
        }
        empezarNivel.onClick.RemoveAllListeners();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PantallaNivelCanvas.SetActive(true);
            empezarNivel.onClick.RemoveAllListeners();

            string nivelActual = nivel;
            textoNivel.text = nivelActual;

            empezarNivel.onClick.AddListener(() => CambioDeNivel(nivelActual));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PantallaNivelCanvas.SetActive(false);
        }
    }

    public void CambioDeNivel(string nivelCargar)
    {
        WeaponManagerDDOL.cargarEscena = true;
        SceneManager.LoadScene(nivelCargar);
    }
}
