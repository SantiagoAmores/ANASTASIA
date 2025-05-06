using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumibleObjeto : MonoBehaviour
{
    public List<GameObject> posiblesObjetos;
    public List<float> probabilidades;

    // Start is called before the first frame update
    void Start()
    {
        InstanciarObjeto();
    }

    // Update is called once per frame
    void InstanciarObjeto()
    {
        float valorRandom = Random.value;
        float acumulador = 0f;

        for (int i = 0; i < probabilidades.Count; i++)
        {
            acumulador += probabilidades[i];
            if (valorRandom <= acumulador)
            {
                Instantiate(posiblesObjetos[i], transform.position, Quaternion.identity);
                break;
            }
        }
    }
}

