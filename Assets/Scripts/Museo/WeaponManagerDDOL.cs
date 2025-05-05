using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManagerDDOL : MonoBehaviour
{
    public static WeaponManagerDDOL instancia;
    public int armaSeleccionada = -1;
    public static bool cargarEscena = false;
    public PantallaIniciarNivel pantalla;

    // Este script se encarga de activar las armas entre escenas
    // Si no hay una instancia previa, se crea una instancia de este script que no se destruye entre escenas, asigna por defecto un arma vacia 
    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
            armaSeleccionada = -1;
            // Llamada a activar el arma por si acaso
            SceneManager.sceneLoaded += activarArmaPorSiAcaso;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pantalla = FindObjectOfType<PantallaIniciarNivel>();
    }

    // Al destruirse el objeto elimina la llamada al activar el arma por si acaso
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= activarArmaPorSiAcaso;
    }

    // En caso de que no haya un arma seleccionada y la escena no sea la de museo, se pone por defecto el arma 0
    void activarArmaPorSiAcaso(Scene scene, LoadSceneMode mode)
    {
        pantalla = FindObjectOfType<PantallaIniciarNivel>();

        if (scene.name != "Scene_Museo" && armaSeleccionada == -1)
        {
            SeleccionarArma(0);
        }
    }

    // Funciones que asignar a botones (cada una es la funcion que hay a continuacion con un indice de arma distinto)
    public void SeleccionarArma0() => SeleccionarArma(0);
    public void SeleccionarArma1() => SeleccionarArma(1);
    public void SeleccionarArma2() => SeleccionarArma(2);
    public void SeleccionarArma3() => SeleccionarArma(3);
    public void SeleccionarArma4() => SeleccionarArma(4);
    public void SeleccionarArma5() => SeleccionarArma(5);

    // Funcion para asignar el arma en si
    public void SeleccionarArma(int index)
    {
        if (pantalla == null)
        {
            pantalla = FindObjectOfType<PantallaIniciarNivel>();
            if (pantalla == null) return;
        }

        pantalla.empezarNivel.gameObject.SetActive(true);

        // Asigna el arma
        armaSeleccionada = index;

        switch (index)
        {
            case 0:
                pantalla.iconoArma1.gameObject.SetActive(true);
                pantalla.iconoArma2.gameObject.SetActive(false);
                pantalla.iconoArma3.gameObject.SetActive(false);
                break;
            case 1:
                pantalla.iconoArma1.gameObject.SetActive(false);
                pantalla.iconoArma2.gameObject.SetActive(false);
                pantalla.iconoArma3.gameObject.SetActive(false);
                break;
            case 2:
                pantalla.iconoArma1.gameObject.SetActive(false);
                pantalla.iconoArma2.gameObject.SetActive(false);
                pantalla.iconoArma3.gameObject.SetActive(false);
                break;
            case 3:
                pantalla.iconoArma1.gameObject.SetActive(false);
                pantalla.iconoArma2.gameObject.SetActive(true);
                pantalla.iconoArma3.gameObject.SetActive(false);
                break;
            case 4:
                pantalla.iconoArma1.gameObject.SetActive(false);
                pantalla.iconoArma2.gameObject.SetActive(false);
                pantalla.iconoArma3.gameObject.SetActive(true);
                break;
            case 5:
                pantalla.iconoArma1.gameObject.SetActive(false);
                pantalla.iconoArma2.gameObject.SetActive(false);
                pantalla.iconoArma3.gameObject.SetActive(false);
                break;
        }
    }
}