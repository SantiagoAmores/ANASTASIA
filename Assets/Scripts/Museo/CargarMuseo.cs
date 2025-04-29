using UnityEngine;
using UnityEngine.UI;

public class CargarMuseo : MonoBehaviour
{
    public Button botonArma0;
    //public Button botonArma1;
    //public Button botonArma2;
    public Button botonArma3;
    public Button botonArma4;
    //public Button botonArma5;
    void Start()
    {
        // Restaura el tiempo por si acaso al volver al museo
        Time.timeScale = 1f;
        WeaponManagerDDOL.cargarEscena = false;

        // Si hay un arma activada, se asigna por defecto un arma vacia
        if (WeaponManagerDDOL.instancia != null)
        {
            WeaponManagerDDOL.instancia.armaSeleccionada = -1;

            // Limpia las funciones de los botones de armas y luego las vuelve a rellenar
            botonArma0.onClick.RemoveAllListeners();
            //botonArma1.onClick.RemoveAllListeners();
            //botonArma2.onClick.RemoveAllListeners();
            botonArma3.onClick.RemoveAllListeners();
            botonArma4.onClick.RemoveAllListeners();
            //botonArma5.onClick.RemoveAllListeners();

            botonArma0.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma0());
            //botonArma1.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma1());
            //botonArma2.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma2());
            botonArma3.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma3());
            botonArma4.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma4());
            //botonArma5.onClick.AddListener(() => WeaponManagerDDOL.instancia.SeleccionarArma5());
        }


    }
}
