using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetosActivables : MonoBehaviour
{
    public static ObjetosActivables instance; // Para que el script ConsumibleObjeto acceda

    public List<Image> imagenesUI; // Lista de imágenes en el Canvas
    public List<GameObject> efectos; // Lista de efectos de los objetos que se activan con "E"

    private int objetoActivo = -1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool ObjetoActivo()
    {
        return objetoActivo != -1; // Para evitar que tenga mas de uno
    }

    public void ActivarObjeto(int id)
    {
        if (id < 0 || id >= imagenesUI.Count) return;

        imagenesUI[id].enabled = true; // Activa la imagen correspondiente en el Canvas
        objetoActivo = id;  // Marca el objeto como activo
    }

    void Update()
    {
        // Al pulsar la E con un objeto activo se usa
        if (Input.GetKeyDown(KeyCode.E) && objetoActivo != -1)
        {
            UsarObjeto(objetoActivo);
        }
    }

    void UsarObjeto(int id)
    {
        if (id < 0 || id >= efectos.Count) return;

        // Activar efecto
        efectos[id].SetActive(true);

        // Desactivar imagen del Canvas
        imagenesUI[id].enabled = false;

        // Desactivar el efecto
        StartCoroutine(DesactivarEfecto(id, 0.5f));

        // Limpiar el objeto activo
        objetoActivo = -1;
    }

    System.Collections.IEnumerator DesactivarEfecto(int id, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        efectos[id].SetActive(false);
    }
}
