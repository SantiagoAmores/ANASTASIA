using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class PapeleraPrefs : MonoBehaviour
{
    public Animator animatorPapelera; // animator de la papelera
    public Opciones opcionesScript; // script de opciones
    public LocaleSelector localeSelector; // Referencia al selector de idioma
    public float duracionAnimacion = 1.0f;

    public bool estaAbierta = false;
    private float tiempoCierre;

    private void OnMouseDown()
    {
        if (estaAbierta) return; // para evitar multiples clicks en la animacion

        // animacion de la papelera
        estaAbierta = true;
        animatorPapelera.SetBool("Abrir", true);

        // Borrar todos los PlayerPrefs
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // Reestablecer los valores por defecto en el script de opciones
        if (opcionesScript != null)
        {
            // Brillo
            opcionesScript.CambiarBrillo(1f);
            opcionesScript.sliderBrillo.value = 1f;

            // Pantalla completa
            bool pantallaCompletaDefault = true;
            opcionesScript.CambiarPantallaCompleta(pantallaCompletaDefault);
            opcionesScript.pantallaCompletaToggle.isOn = pantallaCompletaDefault;

            // Volumen
            opcionesScript.CambiarVolumen(1f);
            opcionesScript.sliderVolumen.value = 1f;

            // cerrar papelera
            tiempoCierre = Time.time + duracionAnimacion;
        }

        // Resetear idioma al predeterminado (primer idioma de la lista)
        if (localeSelector != null)
        {
            // Establecer el idioma predeterminado (0)
            localeSelector.ChangeLocale(2);

            // Forzar la actualización si es necesario
            StartCoroutine(ForzarActualizacionIdioma());
        }
    }

    IEnumerator ForzarActualizacionIdioma()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
    }

    private void Update()
    {
        // Cerrar la papelera  despues del tiempo (para que no se quede abierta)
        if (estaAbierta && Time.time >= tiempoCierre)
        {
            estaAbierta = false;
            animatorPapelera.SetBool("Abrir", false);
        }
    }
}
