using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumerangDestruible : MonoBehaviour
{
    public int golpe;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();

            if (enemigo != null)
            {
                enemigo.RecibirGolpe(golpe, this.gameObject); //le hace daño al enemigo
            }
            //Destroy(other.gameObject); // Destruye al enemigo
        }
    }
}
