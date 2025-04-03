using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PantallaIniciarNivel : MonoBehaviour
{
    public GameObject PantallaNivelCanvas;
    public string nivel;

    void Start()
    {
        PantallaNivelCanvas = GameObject.Find("CanvasSeleccionNivel");
        //PantallaNivelCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PantallaNivelCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PantallaNivelCanvas.SetActive(false);
        }
    }

    public void CambioDeNivel()
    {
        SceneManager.LoadScene(nivel);
    }
}
