using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PantallaIniciarNivel : MonoBehaviour
{
    public GameObject PantallaNivelCanvas;
    public string nivel;
    public Button empezarNivel;

    // NOTA: Cada uno de los triggers incluye el nivel por escrito desde el inspector de Unity.
    // Elimina las funciones del boton de empezar el nivel al principio para evitar bugs, y cada vez que se entra en el collider de uno de los niveles
    // se vuelve a asignar la funcion del nivel teniendo en cuenta el nivel escrito por el inspector

    void Start()
    {
        PantallaNivelCanvas = GameObject.Find("CanvasSeleccionNivel");
        
        empezarNivel = GameObject.Find("EmpezarNivel").GetComponent<Button>();
        empezarNivel.onClick.RemoveAllListeners();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PantallaNivelCanvas.SetActive(true);
            empezarNivel.onClick.RemoveAllListeners();
            empezarNivel.onClick.AddListener(CambioDeNivel);
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
