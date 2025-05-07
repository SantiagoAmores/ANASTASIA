using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;
using Unity.VisualScripting;
using System.Collections;

public class PantallaIniciarNivel : MonoBehaviour
{
    public GameObject PantallaNivelCanvas;
    public string nivel;
    public Button empezarNivel;
    //public TMP_Text textoNivel;
    public TMP_Text armaSeleccionada;

    // Camara para niveles
    private Transform jugadorTransform;
    public Transform targetNivel;
    public CinemachineVirtualCamera museoCamara;
    private Coroutine zoomCoroutine; // Cambio de camara fluida

    public GameObject iconoArma1;
    public GameObject iconoArma2;
    public GameObject iconoArma3;

    public string categoriaDesbloqueo;
    public int indiceDesbloqueo;

    void Start()
    {
        museoCamara = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();

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
    // que es el nombre de la escena a cargar, muestra el nombre del nivel en el canvas y añade el listener que llama a la funcion de cargar la escena
    // y carga la escena del texto que se muestra en el canvas
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!NivelManager.EstaDesbloqueado(categoriaDesbloqueo, indiceDesbloqueo))
            {
                Debug.Log($"Nivel bloqueado: {categoriaDesbloqueo} {indiceDesbloqueo}");
                return;
            }

            PantallaNivelCanvas.SetActive(true);
            empezarNivel.onClick.RemoveAllListeners();

            string nivelActual = nivel;
            //textoNivel.text = nivelActual;

            empezarNivel.onClick.AddListener(() => CambioDeNivel(nivelActual));

            empezarNivel.gameObject.SetActive(false);
            iconoArma1.gameObject.SetActive(false);
            iconoArma2.gameObject.SetActive(false);
            iconoArma3.gameObject.SetActive(false);

            // Camara apuntar cuadros antes de entrar al nivel

            jugadorTransform = other.transform; // Guarda al jugador

            targetNivel = transform.Find("target");

            museoCamara.LookAt = targetNivel.transform;
            museoCamara.Follow = targetNivel.transform;

            // Inicia el zoom suave
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
            zoomCoroutine = StartCoroutine(zoomCuadro(40f, 1f)); // Zoom a 30 en medio segundo
        }
    }

    // Cuando el jugador sale esconde el canvas
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!NivelManager.EstaDesbloqueado(categoriaDesbloqueo, indiceDesbloqueo))
            {
                Debug.Log($"Nivel bloqueado: {categoriaDesbloqueo} {indiceDesbloqueo}");
                return;
            }

            PantallaNivelCanvas.SetActive(false);

            // La camara vuelve a su posicion
            museoCamara.LookAt = jugadorTransform;
            museoCamara.Follow = jugadorTransform;

            // Zoom suave de regreso
            if (zoomCoroutine != null) StopCoroutine(zoomCoroutine);
            zoomCoroutine = StartCoroutine(zoomCuadro(60f, 0.5f));
        }
    }

    public IEnumerator zoomCuadro(float nuevoZoom, float duracion)
    {
        float tiempo = 0f;
        float zoomInicial = museoCamara.m_Lens.FieldOfView;

        while (tiempo < duracion)
        {
            museoCamara.m_Lens.FieldOfView = Mathf.Lerp(zoomInicial, nuevoZoom, tiempo / duracion);
            tiempo += Time.deltaTime;
            yield return null;
        }

        museoCamara.m_Lens.FieldOfView = nuevoZoom;
    }

    // Funcion para cargar la escena
    public void CambioDeNivel(string nivelCargar)
    {
        WeaponManagerDDOL.cargarEscena = true;
        SceneManager.LoadScene(nivelCargar);
    }
}