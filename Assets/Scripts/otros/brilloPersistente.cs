using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brilloPersistente : MonoBehaviour
{
    private static brilloPersistente instancia;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
