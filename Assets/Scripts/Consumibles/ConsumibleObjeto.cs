using System.Collections.Generic;
using UnityEngine;

public class ConsumibleSimple : MonoBehaviour
{
    public List<int> objetoElegido; // Lista de posibles consumibles
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Comprobar si el jugador ya tiene un objeto en su inventario
            if (ObjetosActivables.instance.ObjetoActivo())
            {
                Debug.Log("Ya tienes un objeto. Úsalo antes de recoger otro.");
                return;
            }

            if (objetoElegido.Count == 0) return;

            // Elegir aleatoriamente un objeto de la lista
            int objetoAleatorio = Random.Range(0, objetoElegido.Count);
            int Elegido = objetoElegido[objetoAleatorio];

            Debug.Log("Objeto elegido: " + Elegido);

            // Activar el objeto elegido
            ObjetosActivables.instance.ActivarObjeto(Elegido);
            Destroy(gameObject);
        }
    }
}

