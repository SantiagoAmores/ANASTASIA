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

    private Collider enemigoCollider;

    private void Awake()
    {
        enemigo = GetComponent<NavMeshAgent>();
        enemigoCollider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        canvasManager = FindObjectOfType<CanvasManager>();

        Vector3 miraAnastasia = jugador.transform.position - transform.position;
        miraAnastasia.y = 0f;
        if (miraAnastasia != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(miraAnastasia);
        }

        StartCoroutine(AlSpawnear());

        //animator = GetComponent<Animator>();  // Obtener el Animator del enemigo

        //animator.Play("Walking"); //Animacion

        //// Iniciar la animación de caminar
        //if (animator != null)
        //{
        //    animator.SetBool("Walking", true);
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
            Vector3 puntoDeExperienciaPosicion = new Vector3(transform.position.x, 1f, transform.position.z);
            Instantiate(puntoExperienciaPrefab, puntoDeExperienciaPosicion, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            canvasManager.DerrotaDemo(); //Cambiar Cuando se quite la demo
        }
    }

    public IEnumerator AlSpawnear()
    {
        enemigoCollider.enabled = false;
        enemigo.speed = 0f;

        yield return new WaitForSeconds(0.5f);

        enemigoCollider.enabled = true;
        enemigo.speed = 3.5f;

    }
}
