using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class LibroInteraccion : MonoBehaviour
{
    public GameObject interactionText;  // Texto en 3D que muestra el mensaje de "Presiona E"
    private bool isNearBook = false;  // Si el jugador está cerca del libro
    public CinemachineVirtualCamera bookCamera;  // Referencia a la cámara del libro
    public CinemachineVirtualCamera museoCamara;
    public BestiarioManager bestiarioManager;

    private int normalPriority = 0;
    private int focusPriority = 10;

    void Start()
    {
        // Asegúrate de que el texto no se muestre al inicio
        interactionText.gameObject.SetActive(false);

        // Asegurar que la cámara empiece con prioridad baja
        if (bookCamera != null)
        {
            bookCamera.Priority = normalPriority;
        }
    }

    void Update()
    {
        if (isNearBook && Input.GetKeyDown(KeyCode.E))
        {
            if (bestiarioManager.bestiarioCanvas.activeSelf)
            {
                bestiarioManager.CerrarBestiario();
                Time.timeScale = 1f;
            }
            else
            {
                bestiarioManager.AbrirBestiario();
                Time.timeScale = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearBook = true;
            interactionText.gameObject.SetActive(true);

            if (bookCamera != null)
            {
                museoCamara.Priority = normalPriority;
                bookCamera.Priority = focusPriority;  // Subimos la prioridad para que cambie a esta cámara
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearBook = false;
            interactionText.gameObject.SetActive(false);

            if (bookCamera != null)
            {
                bookCamera.Priority = normalPriority;  // Volvemos a la prioridad normal
                museoCamara.Priority = focusPriority;
            }
        }   
    }
}
