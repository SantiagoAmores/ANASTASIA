using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    public int idColeccionable; // 0, 1 o 2 según el nivel
    private string categoria = "coleccionables";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Marcar como desbloqueado en PlayerPrefs
            string clave = $"desbloqueo_{categoria}_{idColeccionable}";
            PlayerPrefs.SetInt(clave, 1);
            PlayerPrefs.Save();

            Debug.Log($"Coleccionable {idColeccionable} recogido y desbloqueado en el bestiario");

            Destroy(gameObject); // Eliminar el objeto del mundo
        }
    }
}
