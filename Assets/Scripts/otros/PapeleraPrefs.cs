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

    public GameObject panelConfirmacionBorrar;

    public bool estaAbierta = false;
    private float tiempoCierre;

    private void OnMouseDown()
    {
        if (estaAbierta) return;

        // Mostrar panel de confirmación
        panelConfirmacionBorrar.SetActive(true);
    }

    public void ConfirmarBorrado()
    {
        estaAbierta = true;
        animatorPapelera.SetBool("Abrir", true);

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (opcionesScript != null)
        {
            opcionesScript.CambiarBrillo(1f);
            opcionesScript.sliderBrillo.value = 1f;

            bool pantallaCompletaDefault = true;
            opcionesScript.CambiarPantallaCompleta(pantallaCompletaDefault);
            opcionesScript.pantallaCompletaToggle.isOn = pantallaCompletaDefault;

            opcionesScript.CambiarVolumen(1f);
            opcionesScript.sliderVolumen.value = 1f;

            tiempoCierre = Time.time + duracionAnimacion;
        }

        if (localeSelector != null)
        {
            localeSelector.ChangeLocale(0);
            StartCoroutine(ForzarActualizacionIdioma());
        }

        panelConfirmacionBorrar.SetActive(false); // Cerrar el panel
    }

    public void CancelarBorrado()
    {
        panelConfirmacionBorrar.SetActive(false);
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
