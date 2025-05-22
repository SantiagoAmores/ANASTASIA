using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarDialogo : MonoBehaviour
{
    public GameObject bocadillo;

    private void Start()
    {
        bocadillo.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bocadillo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bocadillo.SetActive(false);
        }
    }
}
