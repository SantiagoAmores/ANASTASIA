using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumerangDestruible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // Destruye al enemigo
        }
    }
}
