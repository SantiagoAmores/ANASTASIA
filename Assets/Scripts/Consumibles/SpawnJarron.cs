using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnJarron : MonoBehaviour
{
    public GameObject objetoAInstanciar;
    private StatsEnemigos stats;

    void Start()
    {
        stats = GetComponent<StatsEnemigos>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.enemigoVida <= 0)
        {
            InstanciarObjeto();
            Destroy(gameObject); // Destruye el jarrón
        }
    }

    void InstanciarObjeto()
    {
        Instantiate(objetoAInstanciar, transform.position, Quaternion.identity);
    }
}
