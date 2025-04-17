using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharcoPintura : MonoBehaviour
{
    public int golpe;
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemigo enemigo = other.GetComponent<Enemigo>();
            if (enemigo != null)
            {

                enemigo.RecibirGolpe(golpe);

                heridaPausa();

            }

        }
    }

    // Para evitar que reciba golpe todo el rato cuando esta dentro del charco
    private IEnumerator heridaPausa()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
