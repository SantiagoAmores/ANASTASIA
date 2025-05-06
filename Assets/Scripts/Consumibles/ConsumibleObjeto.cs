using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConsumibleSimple : MonoBehaviour
{
    public List<int> objetoElegido; // Lista de posibles consumibles
    public CanvasManager canvasManager; //Donde están los sprites con los efectos de los objetos

    private void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MovimientoJugador jugador = other.GetComponent<MovimientoJugador>();

            // Si ya tiene un objeto activo, no recoger otro
            if (jugador.tieneObjetoActivo)
            {
                Debug.Log("Ya tienes un objeto. Úsalo antes de recoger otro.");
                return;
            }

            if (objetoElegido.Count == 0) return;

            // Elegir aleatoriamente un objeto de la lista
            int objetoAleatorio = Random.Range(0, objetoElegido.Count);
            int elegido = objetoElegido[objetoAleatorio];

            canvasManager.objetoActivable.SetActive(true);

            switch (elegido)
            {
                case 0:
                 
                    canvasManager.objeto1.SetActive(true);
                    break;
                case 1:

                    canvasManager.objeto2.SetActive(true);
                    break;
                case 2:

                    canvasManager.objeto3.SetActive(true);
                    break;
                case 3:

                    canvasManager.objeto4.SetActive(true);
                    break;
                case 4:

                    canvasManager.objeto5.SetActive(true);
                    break;
            }

            jugador.tieneObjetoActivo = true;
            jugador.objetoActual = elegido;

            Debug.Log("Objeto elegido: " + elegido);

            Destroy(gameObject);
        }
    }
}

