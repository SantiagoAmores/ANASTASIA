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

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        canvasManager = FindObjectOfType<CanvasManager>();

        //animator = GetComponent<Animator>();  // Obtener el Animator del enemigo

        //animator.Play("Walking"); //Animacion

        //// Iniciar la animaci�n de caminar
        //if (animator != null)
        //{
        //    animator.SetBool("isWalking", true);  // Aseg�rate de que 'isWalking' es un par�metro booleano en tu Animator
        //}

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
