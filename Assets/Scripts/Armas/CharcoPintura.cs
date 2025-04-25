using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcoPintura : MonoBehaviour
{
    public int golpe;

    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {

                enemigo.RecibirGolpe(golpe, this.gameObject);

            }

        }
    }

}
