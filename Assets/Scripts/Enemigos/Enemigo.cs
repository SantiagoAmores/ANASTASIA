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

    //private Animator animator;

    private Collider enemigoCollider;


    // ESTADISTICAS
    public int      enemigoVidaTotal;
    public float    enemigoVelocidad;
    public int      enemigoAtaque;
    public int      enemigoExperiencia;

    private StatsEnemigos estadisticas;

    public int enemigoVidaActual;

    private void Awake()
    {
        enemigo = GetComponent<NavMeshAgent>();
        enemigoCollider = GetComponent<Collider>();
    }

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        canvasManager = FindObjectOfType<CanvasManager>();

        estadisticas = GetComponent<StatsEnemigos>();
        estadisticas.revisarEnemigo();

        enemigoVidaTotal = estadisticas.enemigoVida;
        enemigoAtaque = estadisticas.enemigoAtaque;
        enemigoVelocidad = estadisticas.enemigoVelocidad;
        enemigoExperiencia = estadisticas.enemigoExperiencia;

        enemigoVidaActual = enemigoVidaTotal;
        enemigo.speed = enemigoVelocidad;

        Vector3 miraAnastasia = jugador.transform.position - transform.position;
        miraAnastasia.y = 0f;
        if (miraAnastasia != Vector3.zero) { transform.rotation = Quaternion.LookRotation(miraAnastasia); }

        StartCoroutine(AlSpawnear());

        /*
        animator = GetComponent<Animator>();  // Obtener el Animator del enemigo

        animator.Play("Walking"); //Animacion

        // Iniciar la animación de caminar
        if (animator != null)
        {
            animator.SetBool("Walking", true);
        }
        */
    }

    void Update()
    {
        enemigo.SetDestination(jugador.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            enemigoVidaActual--;
            if (enemigoVidaActual <= 0)
            {
                float radioDrop = 1f;
                float alturaFija = 0.5f;
                for (int i = 0; i < enemigoExperiencia; i++)
                {
                    Vector2 offset2D = Random.insideUnitCircle * radioDrop;
                    Vector3 dropPosicion = new Vector3(
                        transform.position.x + offset2D.x,
                        alturaFija,
                        transform.position.z + offset2D.y
                        );
                    Instantiate(puntoExperienciaPrefab, dropPosicion, Quaternion.identity);
                }
                Destroy(this.gameObject);

            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            canvasManager.Derrota();
        }
    }

    public IEnumerator AlSpawnear()
    {
        enemigoCollider.enabled = false;
        enemigo.speed = 0f;

        yield return new WaitForSeconds(1f);

        enemigoCollider.enabled = true;
        enemigo.speed = 2.5f;
    }
}
