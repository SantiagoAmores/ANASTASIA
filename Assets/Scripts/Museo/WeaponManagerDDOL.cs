using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponManagerDDOL : MonoBehaviour
{
    public static WeaponManagerDDOL instancia;
    public int armaSeleccionada = -1;
    public static bool cargarEscena = false;
    public TextMeshProUGUI textoArma;

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

    // Al destruirse el objeto elimina la llamada al activar el arma por si acaso
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= activarArmaPorSiAcaso;
    }

    // En caso de que no haya un arma seleccionada y la escena no sea la de museo, se pone por defecto el arma 0
    void activarArmaPorSiAcaso(Scene scene, LoadSceneMode mode)
    {
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
        // Busca el texto del arma en la interfaz
        if (textoArma == null)
        {
            textoArma = GameObject.Find("textoArma")?.GetComponent<TextMeshProUGUI>();
        }

        // Avisa por la interfaz el arma elegida
        if (textoArma != null)
        {
            textoArma.text = "Arma numero " + index + " seleccionada.";
        }

        // Asigna el arma
        armaSeleccionada = index;
    }
}
