using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArma : MonoBehaviour
{
    public void DropDelArma()
    {
        GameObject inventarioDeAnastasia = GameObject.Find("Inventario");

        string nombreJefe = gameObject.name.Replace("(Clone)", "").Trim();

        switch (nombreJefe)
        {
            case "Enemigo 3":
                inventarioDeAnastasia.GetComponent<Arma2>().enabled = true;
                break;
            case "Enemigo 6":
                inventarioDeAnastasia.GetComponent<Arma3>().enabled = true;
                break;
            case "Enemigo 9":
                inventarioDeAnastasia.GetComponent<Arma6>().enabled = true;
                break;
            default:
                break;
        }
    }
}
