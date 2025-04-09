using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonSeleccionArma : MonoBehaviour
{
    public int armaIndex;

    public void SeleccionarYEmpezarNivel()
    {
        Debug.Log(armaIndex);
        WeaponManagerDDOL.instancia.SeleccionarArma(armaIndex);
    }
}