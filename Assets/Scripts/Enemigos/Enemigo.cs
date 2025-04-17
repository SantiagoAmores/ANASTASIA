using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    public NavMeshAgent enemigo;

    public GameObject jugador;
    private MovimientoJugador moverJugador;
    public GameObject puntoExperienciaPrefab;

    private CanvasManager canvasManager;

    //private Animator animator;

    private Collider enemigoCollider;

    // ESTADISTICAS
    public int      enemigoVidaTotal;
    public float    enemigoVelocidad;
    public int      enemigoAtaque;
    public int      enemigoExperiencia;

    public int enemigoVidaActual;

    private StatsEnemigos estadisticas;

    private void Awake()
    {
        enemigo = GetComponent<NavMeshAgent>();
        enemigoCollider = GetComponent<Collider>();
    }

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
        moverJugador = jugador.GetComponent<MovimientoJugador>();
        canvasManager = FindObjectOfType<CanvasManager>();

        // Estadisticas del enemigo
        estadisticas = GetComponent<StatsEnemigos>();
        // Busca al enemigo a partir del nombre de su prefab y le otorga sus respectivas estadisticas
        estadisticas.revisarEnemigo();

        enemigoVidaTotal    = estadisticas.enemigoVida;
        enemigoAtaque       = estadisticas.enemigoAtaque;
        enemigoVelocidad    = estadisticas.enemigoVelocidad;
        enemigoExperiencia  = estadisticas.enemigoExperiencia;

        // Asigna su salud actual = su vida total
        enemigoVidaActual   = enemigoVidaTotal;

        // Asigna la velocidad del navMeshAgent = su velocidad
        enemigo.speed       = enemigoVelocidad;

        // Hace que el enemigo mire hacia Anastasia al ser instanciado
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
        if (other.gameObject.CompareTag("Player"))
        {
            moverJugador.herirAnastasia(enemigoAtaque);
            AnimacionHerida();
        }
    }

    IEnumerator AnimacionHerida()
    {
        //SkinnedMeshRenderer [] anastasiaPiezas;

        //anastasiaPiezas = jugador.GetComponentsInChildren<SkinnedMeshRenderer>();

        //foreach (var pieza in anastasiaPiezas)
        //{
        //    pieza.material.color = Color.red;
        //}

        yield return new WaitForSeconds(0.5f);

        //foreach (var pieza in anastasiaPiezas)
        //{
        //    pieza.material.color = Color.white;
        //}
    }

    public void RecibirGolpe(int cantidadDeGolpe)
    {
        // Le resta al enemigo de su salud el daño que hace el golpe recibido
        enemigoVidaActual -= cantidadDeGolpe;

        // Si la salud del enemigo es igual o baja de 0
        if (enemigoVidaActual <= 0)
        {
            // Genera un area pequeña con una altura fija
            float radioDrop = 1f;
            float alturaFija = 0.5f;
            // Y dependiendo del int enemigoExperiencia
            for (int i = 0; i < enemigoExperiencia; i++)
            {
                // Instanciara en lugares aleatorios del area su respectiva cantidad de puntos de experiencia
                Vector2 offset2D = Random.insideUnitCircle * radioDrop;
                Vector3 dropPosicion = new Vector3(
                    transform.position.x + offset2D.x,
                    alturaFija,
                    transform.position.z + offset2D.y
                    );
                Instantiate(puntoExperienciaPrefab, dropPosicion, Quaternion.identity);
            }
            // Y despues destruye al enemigo
            Destroy(this.gameObject);
        }
    }

    public IEnumerator AlSpawnear()
    {
        // Inhabilita temporalmente el collider y el movimiento del enemigo para que no le hagan daño a Anastasia
        // nada mas aparecer. Despues vuelve a activar el collider y le asigna su velocidad correspondiente
        enemigoCollider.enabled = false;
        enemigo.speed = 0f;

        yield return new WaitForSeconds(0.33f);

        enemigoCollider.enabled = true;
        enemigo.speed = enemigoVelocidad;
    }
}
