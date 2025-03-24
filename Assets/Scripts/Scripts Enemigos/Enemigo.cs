using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public NavMeshAgent enemigo;
    public GameObject jugador;

    public GameObject puntoExperienciaPrefab;

    private CanvasManager canvasManager;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Jugador");
        canvasManager = FindObjectOfType<CanvasManager>();
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
            Instantiate(puntoExperienciaPrefab, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            canvasManager.Derrota();
        }
    }
}
