using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetosActivables : MonoBehaviour
{
    public static ObjetosActivables instance; // Para que el script ConsumibleObjeto acceda

    public List<Image> imagenesObjetos; // Lista de imágenes en el Canvas
    public List<GameObject> efectosObjetos; // Lista los objetos que se activan

    private int objetoActivo = -1;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    public bool ObjetoActivo()
    {
        return objetoActivo != -1; // Para evitar que tenga mas de uno
    }

    public void ActivarObjeto(int id)
    {
        if (id < 0 || id >= imagenesObjetos.Count || id >= efectosObjetos.Count) return;

        // Activar imagen en el Canvas
        imagenesObjetos[id].enabled = true;

        // Instanciar el objeto correspondiente
        if (efectosObjetos[id] != null)
        {
            Instantiate(efectosObjetos[id]);
        }

        objetoActivo = id;
    }

    public void UsarObjeto(int id)
    {
        if (objetoActivo < 0 || objetoActivo >= imagenesObjetos.Count) return;

        // Desactiva la imagen del Canvas
        imagenesObjetos[objetoActivo].enabled = false;

        // Desactiva el gameobject de la lista
        objetoActivo = -1;
    }

    // Corutina para desactivar el efecto después de un tiempo
    IEnumerator DesactivarEfecto(int id, float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        efectosObjetos[id].SetActive(false);  // Desactivar el GameObject del efecto
    }
}
