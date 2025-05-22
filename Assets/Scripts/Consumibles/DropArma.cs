using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DropArma : MonoBehaviour
{
    public void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject inventarioDeAnastasia = GameObject.Find("Inventario");

            // Obtener el nombre de la escena actual
            string nombreEscena = SceneManager.GetActiveScene().name;

            switch (nombreEscena)
            {
                case "Scene_Nivel_1":
                    inventarioDeAnastasia.GetComponent<Arma2>().enabled = true;
                    break;
                case "Scene_Nivel_2":
                    inventarioDeAnastasia.GetComponent<Arma3>().enabled = true;
                    break;
                case "Scene_Nivel_3":
                    inventarioDeAnastasia.GetComponent<Arma6>().enabled = true;
                    break;
                default:
                    break;
            }

            Destroy(gameObject);

        }
        
    }
}
