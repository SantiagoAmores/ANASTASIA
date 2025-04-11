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

    void Start()
    {
        // Oculta la ventana de la interfaz
        PantallaNivelCanvas.SetActive(false);
        
        // Por si acaso busca el boton
        if (empezarNivel == null)
        {
            empezarNivel = PantallaNivelCanvas.GetComponentInChildren<Button>();
        }

        // Elimina las listeners previos del boton, para que se asigne en OnTriggerEnter
        empezarNivel.onClick.RemoveAllListeners();
    }

    // Cuando el jugador entra en el trigger, muestra el canvas, vuelve a eliminar los listeners por si aca, coge el texto escrito manualmente en el inspector,
    // que es el nombre de la escena a cargar, muestra el nombre del nivel en el canvas y a�ade el listener que llama a la funcion de cargar la escena
    // y carga la escena del texto que se muestra en el canvas
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

    // Cuando el jugador sale esconde el canvas
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PantallaNivelCanvas.SetActive(false);
        }
    }

    // Funcion para cargar la escena
    public void CambioDeNivel(string nivelCargar)
    {
        WeaponManagerDDOL.cargarEscena = true;
        SceneManager.LoadScene(nivelCargar);
    }
}
