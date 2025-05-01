using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScriptJarron : MonoBehaviour
{
    public GameObject objetoAInstanciar;
    private StatsEnemigos stats; // se le puso uno de vida para que anastasia lo ataque y se destruya

    void Start()
    {
        stats = GetComponent<StatsEnemigos>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.enemigoVida <= 0)
        {
            Instantiate(objetoAInstanciar, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destruye el jarrón cuando su vida es cero
        }
    }

    void InstanciarObjeto()
    {
        Instantiate(objetoAInstanciar, transform.position, Quaternion.identity); // Se instancia el objeto consumible
    }
}
