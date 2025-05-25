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

            CanvasManager canvasManager = GameObject.FindObjectOfType<CanvasManager>();

            if (PlayerPrefs.GetInt("desbloqueo_niveles_4") == 1)
            {
                ArmasAleatorias();
                Destroy(gameObject);
                return;
            }
            else
            {
                // Obtener el nombre de la escena actual
                string nombreEscena = SceneManager.GetActiveScene().name;

                switch (nombreEscena)
                {
                    case "Scene_Nivel_1":
                        inventarioDeAnastasia.GetComponent<Arma2>().enabled = true;
                        canvasManager.ActivarArmaEnInterfaz(1);
                        break;
                    case "Scene_Nivel_2":
                        inventarioDeAnastasia.GetComponent<Arma3>().enabled = true;
                        canvasManager.ActivarArmaEnInterfaz(2);
                        break;
                    case "Scene_Nivel_3":
                        inventarioDeAnastasia.GetComponent<Arma6>().enabled = true;
                        canvasManager.ActivarArmaEnInterfaz(5);
                        break;
                    default:
                        break;
                }
            }
            Destroy(gameObject);
        }
    }

    private void ArmasAleatorias()
    {
        GameObject inventarioDeAnastasia = GameObject.Find("Inventario");

        CanvasManager canvasManager = GameObject.FindObjectOfType<CanvasManager>();

        int totalArmas = 6;

        List<int> armasDisponibles = new List<int>();
        for (int i = 0; i < totalArmas; i++)
        {
            bool estaActiva = false;
            switch (i)
            {
                case 0:
                    estaActiva = inventarioDeAnastasia.GetComponent<Arma1>().enabled;
                    break;
                case 1:
                    estaActiva = inventarioDeAnastasia.GetComponent<Arma2>().enabled;
                    break;
                case 2:
                    estaActiva = inventarioDeAnastasia.GetComponent<Arma3>().enabled;
                    break;
                case 3:
                    estaActiva = inventarioDeAnastasia.GetComponent<Arma4>().enabled;
                    break;
                case 4:
                    estaActiva = inventarioDeAnastasia.GetComponent<Arma5>().enabled;
                    break;
                case 5:
                    estaActiva = inventarioDeAnastasia.GetComponent<Arma6>().enabled;
                    break;
            }

            if (!estaActiva)
            {
                armasDisponibles.Add(i);
            }
        }

        if (armasDisponibles.Count == 0)
        {
            return;
        }

        int armaAleatoriaIndex = armasDisponibles[Random.Range(0, armasDisponibles.Count)];

        switch (armaAleatoriaIndex)
        {
            case 0:
                inventarioDeAnastasia.GetComponent<Arma1>().enabled = true;
                break;
            case 1:
                inventarioDeAnastasia.GetComponent<Arma2>().enabled = true;
                break;
            case 2:
                inventarioDeAnastasia.GetComponent<Arma3>().enabled = true;
                break;
            case 3:
                inventarioDeAnastasia.GetComponent<Arma4>().enabled = true;
                break;
            case 4:
                inventarioDeAnastasia.GetComponent<Arma5>().enabled = true;
                break;
            case 5:
                inventarioDeAnastasia.GetComponent<Arma6>().enabled = true;
                break;
        }
        canvasManager.ActivarArmaEnInterfaz(armaAleatoriaIndex);
    }
}
