using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CanvasPintura : MonoBehaviour
{
    public float tiempoTotal = 200f;
    public float intervalo = 50f;
    public Image[] imagenes;
    public RectTransform canvasRectTransform; //Transform pero para elementos del canvas

    private float temporizador = 0f;
    private int indiceActual = 0;

    void Start()
    {  // Desactivar todas las imágenes al inicio
        foreach (Image img in imagenes)
        {
            if (img != null)
            {
                img.gameObject.SetActive(false);

                // Añadir componente de interacción para clicks
                if (img.GetComponent<Button>() == null)
                {
                    Button btn = img.gameObject.AddComponent<Button>();
                    btn.onClick.AddListener(() => OcultarImagen(img));
                }
            }
        }
    }

    void Update()
    {
        temporizador += Time.deltaTime;

        if (indiceActual < imagenes.Length && temporizador >= (indiceActual + 1) * intervalo)
        {
            MostrarImagen(indiceActual);
            indiceActual++;
        }

        if (temporizador >= tiempoTotal)
        {
            enabled = false;
        }
    }

    void MostrarImagen(int indice)
    {
        if (imagenes[indice] != null)
        {
            RectTransform imgRect = imagenes[indice].rectTransform;

            // Posición aleatoria
            Vector2 nuevaPos = GenerarPosicionAleatoria(imgRect);
            imgRect.anchoredPosition = nuevaPos;

            // Activar y escalar desde 0 a 1
            imagenes[indice].gameObject.SetActive(true);
            imgRect.localScale = Vector3.zero;
            StartCoroutine(AnimarEscala(imgRect));
        }
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

        obj.localScale = Vector3.one;
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
