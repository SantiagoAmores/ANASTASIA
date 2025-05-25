using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArmasEnInterfaz : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (FindObjectOfType<SpawnEnemigos>() == null)
        {
            return;
        }

        StartCoroutine(AgregarArmaInterfaz());
    }

    private IEnumerator AgregarArmaInterfaz()
    {
        yield return null;
        CanvasManager canvas = FindObjectOfType<CanvasManager>();
        if (canvas != null && WeaponManagerDDOL.instancia != null)
        {
            int index = WeaponManagerDDOL.instancia.armaSeleccionada;
            canvas.ActivarArmaEnInterfaz(index);
        }
    }
}