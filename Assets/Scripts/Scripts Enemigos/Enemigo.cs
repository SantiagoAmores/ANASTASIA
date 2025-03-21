using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public NavMeshAgent enemigo;
    public GameObject jugador;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Jugador");
    }

    // Update is called once per frame
    void Update()
    {
        enemigo.SetDestination(jugador.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            Destroy(this.gameObject);
        }
    }
}
