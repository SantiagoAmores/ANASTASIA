using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Jefe03 : MonoBehaviour
{
    Enemigo enemigoScript;
    StatsEnemigos statsScript;
    Animator animator;
    public GameObject jugador;
    private NavMeshAgent navMeshAgent;

    // Variables de movimiento y ataque
    private bool isAttacking = false;

    // Variables para movimiento aleatorio (fase 2)
    public List<Transform> puntosMovimiento = new List<Transform>();

    // Variables de aceleraci�n
    private float currentSpeed = 0f;
    private float acceleration = 5f;
    private float deceleration = 8f;
    private float maxSpeedPhase1 = 7f;
    private float maxSpeedPhase2 = 10f;

    // Variables para controlar la dirección hacia el jugador
    private Vector3 direccionJugador;
    private bool direccionCalculada = false;

    public GameObject llamaPrefab; // Asigna el prefab desde el inspector
    public float intervaloLlamas = 0.5f; // Tiempo entre cada llama
    private Vector3 ultimaPosicionFuego;
    private float distanciaEntreLlamas = 1f; 


    void Start()
    {
        enemigoScript = GetComponent<Enemigo>();
        statsScript = GetComponent<StatsEnemigos>();
        animator = GetComponentInChildren<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();

        maxSpeedPhase1 = statsScript.enemigoVelocidad;
        maxSpeedPhase2 = statsScript.enemigoVelocidad;

        GameObject[] puntos = GameObject.FindGameObjectsWithTag("PuntoMovimiento");
        foreach (GameObject punto in puntos)
        {
            if (punto.activeInHierarchy && punto.transform.position != Vector3.zero)
            {
                puntosMovimiento.Add(punto.transform);
            }
        }

        // Se puede configurar visualmente en el prefab si comienza en fase 1 o 2
        if (statsScript.faseDeJefe == 2)
        {
            transform.localScale *= 1.5f;
        }

        statsScript.revisarEnemigo();
        StartCoroutine(ComportamientoJefe());
    }

    void Update()
    {
        animator.SetFloat("velocidadActual", enemigoScript.enemigo.speed);

        // Solo mirar al jugador si no está atacando puntos
        if (!isAttacking)
        {
            enemigoScript.MirarAnastasia(); // ← O desactívalo si mueve al enemigo
        }

        if (statsScript.faseDeJefe == 1 &&
            statsScript.enemigoVida <= statsScript.enemigoVida * 0.5f)
        {
            CambiarAFase2();
        }
    }

    // Se puede llamar desde otros scripts si lo quer�is activar de forma externa
    public void CambiarAFase2()
    {
        statsScript.faseDeJefe = 2;
        statsScript.revisarEnemigo(); // Cargar nuevas stats
        transform.localScale *= 1.5f;
        navMeshAgent.speed = statsScript.enemigoVelocidad;
        animator.SetTrigger("CambioFase");
    }

    public IEnumerator ComportamientoJefe()
    {
        while (true)
        {
            statsScript.revisarEnemigo();

            if (statsScript.faseDeJefe == 1)
            {
                // Movimiento recto, tipo embestida
                yield return StartCoroutine(MovimientoLineal());
                yield return new WaitForSeconds(1.5f);
            }
            else if (statsScript.faseDeJefe == 2)
            {
                yield return StartCoroutine(MovimientoPuntosAleatorios());
                yield return new WaitForSeconds(0.8f);
            }
        }
    }

    private IEnumerator MovimientoLineal()
    {
        isAttacking = true;
        animator.SetBool("atacando", true);

        if (!direccionCalculada)
        {
            direccionJugador = (jugador.transform.position - transform.position).normalized;
            direccionCalculada = true;
        }

        float attackDuration = 3f;
        float timer = 0f;

        while (timer < attackDuration)
        {
            timer += Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeedPhase1);
            transform.position += direccionJugador * currentSpeed * Time.deltaTime;
            yield return null;
        }

        while (currentSpeed > 0.1f)
        {
            currentSpeed -= deceleration * Time.deltaTime;
            transform.position += direccionJugador * currentSpeed * Time.deltaTime;
            yield return null;
        }

        currentSpeed = 0f;
        isAttacking = false;
        animator.SetBool("atacando", false);
        direccionCalculada = false;
    }


    private IEnumerator MovimientoPuntosAleatorios()
    {
        isAttacking = true;
        enemigoScript.seguirJugador = false;

        navMeshAgent.speed = maxSpeedPhase2;

        while (true) // bucle para seguir los puntos aleatorios
        {

            List<Transform> puntosDisponibles = new List<Transform>(puntosMovimiento);
            int puntosVisitados = 0;

            while (puntosVisitados < 5)
            {
                if (puntosDisponibles.Count == 0) break;

                int index = Random.Range(0, puntosDisponibles.Count);
                Transform destino = puntosDisponibles[index];
                puntosDisponibles.RemoveAt(index);

                navMeshAgent.SetDestination(destino.position);

                ultimaPosicionFuego = transform.position;

                animator.SetBool("atacando", true); // Activar animación de movimiento

                // Esperar a que llegue al destino
                while (Vector3.Distance(transform.position, destino.position) > 0.5f)
                {
                    float distanciaRecorrida = Vector3.Distance(transform.position, ultimaPosicionFuego);

                    if (distanciaRecorrida >= distanciaEntreLlamas)
                    {
                        Vector3 posicionFuego = transform.position;
                        posicionFuego.y = 0.5f; // Pegado al suelo

                        // Calcular dirección de movimiento
                        Vector3 direccionMovimiento = (transform.position - ultimaPosicionFuego).normalized;

                        // Calcular ángulo en grados
                        float angulo = Mathf.Atan2(direccionMovimiento.x, direccionMovimiento.z) * Mathf.Rad2Deg;

                        // Crear la rotación
                        Quaternion rotacionFuego = Quaternion.Euler(0, angulo + 90f, 0);

                        // Instanciar con rotación
                        Instantiate(llamaPrefab, posicionFuego, rotacionFuego);

                        ultimaPosicionFuego = transform.position;
                    }

                    yield return null;
                }

                // Llegó al destino
                animator.SetBool("atacando", false); // Cambiar a animación idle

                yield return new WaitForSeconds(2f); // Esperar 2 segundo

                puntosVisitados++;
            }

            yield return new WaitForSeconds(4f); // Espera 4 segundos tras 5 puntos
        }
    }
}
