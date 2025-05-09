using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasPintura : MonoBehaviour
{
    public Image[] imagenes;
    public RectTransform canvasRectTransform; //Transform pero para elementos del canvas
    Vector3 escalaFinal = new Vector3(3f, 3f, 3f);

    void Start()
    {  
        // Desactivar todas las imágenes al inicio
        foreach (Image img in imagenes)
        {
            if (img != null)
            {
                img.gameObject.SetActive(false);
                img.rectTransform.localScale = Vector3.zero;

                // Añadir componente de interacción para clicks
                if (img.GetComponent<Button>() == null)
                {
                    Button btn = img.gameObject.AddComponent<Button>();
                    btn.onClick.AddListener(() => OcultarImagen(img));
                }
            }
        }
    }
    public void PrimeraFase()
    {
        ActivarImagenesAleatorias(2);
    }

    public void SegundaFase()
    {
        ActivarImagenesAleatorias(4);
    }

    private void ActivarImagenesAleatorias(int cantidad)
    {
        List<int> indicesDisponibles = new List<int>();
        for (int i = 0; i < imagenes.Length; i++) indicesDisponibles.Add(i);

        for (int i = 0; i < cantidad; i++)
        {
            if (indicesDisponibles.Count == 0) break;

            int randomIndex = Random.Range(0, indicesDisponibles.Count);
            int imagenIndex = indicesDisponibles[randomIndex];
            indicesDisponibles.RemoveAt(randomIndex);

            Image img = imagenes[imagenIndex];
            MostrarImagen(img);
        }
    }

    void MostrarImagen(Image img)
    {
         RectTransform imgRect = img.rectTransform;
         imgRect.anchoredPosition = GenerarPosicionAleatoria(imgRect);

         img.gameObject.SetActive(true);
         StartCoroutine(AnimarEscala(imgRect));
         StartCoroutine(DesaparecerAut(img));
        
    }

    IEnumerator AnimarEscala(RectTransform obj)
    {
        float duracion = 0.3f;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float t = tiempo / duracion;
            obj.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            tiempo += Time.deltaTime;
            yield return null;
        }

        obj.localScale = escalaFinal;
    }
    private IEnumerator DesaparecerAut(Image img)
    {
        yield return new WaitForSeconds(8f);
        OcultarImagen(img);
    }

    void OcultarImagen(Image img)
    {
        img.gameObject.SetActive(false);
    }

    Vector2 GenerarPosicionAleatoria(RectTransform imagen)
    {
        Vector2 canvasSize = canvasRectTransform.rect.size;
        Vector2 imagenSize = imagen.rect.size;

        float margenX = imagenSize.x / 2f;
        float margenY = imagenSize.y / 2f;

        float x = Random.Range(-canvasSize.x / 2f + margenX, canvasSize.x / 2f - margenX);
        float y = Random.Range(-canvasSize.y / 2f + margenY, canvasSize.y / 2f - margenY);

        return new Vector2(x, y);
    }

}
