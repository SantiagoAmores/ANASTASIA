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

    public GameObject rayoPrefab; 
    public float velocidadRayoFase1 = 30f; // Velocidad de rotación del rayo en fase 1
    public float velocidadRayoFase2 = 15f; // Velocidad de rotación del rayo en fase 2

    public bool mirando = false;


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
        // Solo fija velocidad a 0.1f si está atacando y no usando NavMesh
        if (isAttacking && navMeshAgent.isStopped)
        {
            animator.SetFloat("velocidadActual", 0.1f, 0.1f, Time.deltaTime * 5f);
        }
        else
        {
            float targetSpeed = navMeshAgent.velocity.magnitude;
            animator.SetFloat("velocidadActual", targetSpeed, 0.1f, Time.deltaTime * 5f);
        }

        if (mirando)
        {
            enemigoScript.MirarAnastasia();
        }

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
                yield return StartCoroutine(MovimientoLineal()); // Embestida 1
                yield return StartCoroutine(MovimientoLineal()); // Embestida 2
                yield return StartCoroutine(AtaqueRayoFase1());  // Rayo hacia el jugador
            }

            else if (statsScript.faseDeJefe == 2)
            {
                yield return StartCoroutine(MovimientoPuntosAleatorios()); // Se mueve a 5 puntos
                yield return StartCoroutine(AtaqueRayosFase2());           // Lanza 4 rayos giratorios
            }

        }
    }

    private IEnumerator MovimientoLineal()
    {
        isAttacking = true;
        animator.SetBool("atacando", true);
        navMeshAgent.isStopped = false;
        enemigoScript.seguirJugador = false;
        enemigoScript.MirarAnastasia();

        Vector3 direccion = (jugador.transform.position - transform.position).normalized;
        direccion.y = 0f; // evita inclinación en el eje Y

        if (direccion.magnitude > 0.1f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);
        }

        if (!direccionCalculada)
        {
            direccionJugador = (jugador.transform.position - transform.position).normalized;
            direccionCalculada = true;
        }

        float attackDuration = 3.5f;
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

        yield return new WaitForSeconds(0.5f); // pausa tras embestida

        currentSpeed = 0f;
        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("atacando", false);
        direccionCalculada = false;
    }


    private IEnumerator MovimientoPuntosAleatorios()
    {
        isAttacking = true;
        animator.SetBool("atacando", true);

        enemigoScript.seguirJugador = false;
        navMeshAgent.speed = maxSpeedPhase2;

        // Obtener todos los puntos de movimiento por tag
        GameObject[] puntosArray = GameObject.FindGameObjectsWithTag("PuntoMovimiento");
        List<Transform> puntosDisponibles = new List<Transform>();

        Transform puntoFinal = null;

        // Separar el punto final (EnemigoPuntosMov (5)) del resto
        foreach (GameObject punto in puntosArray)
        {
            if (punto.name == "EnemigoPuntosMov (5)")
            {
                puntoFinal = punto.transform;
            }
            else
            {
                puntosDisponibles.Add(punto.transform);
            }
        }

        // Si no se encontró el punto final, lanzar un error
        if (puntoFinal == null)
        {
            Debug.LogError("No se encontró el punto final 'EnemigoPuntosMov (5)'");
            yield break;
        }

        int puntosVisitados = 0;

        // Mover a 4 puntos aleatorios (el 5º será el fijo)
        while (puntosVisitados < 4 && puntosDisponibles.Count > 0)
        {
            animator.SetBool("atacando", true);

            int index = Random.Range(0, puntosDisponibles.Count);
            Transform destino = puntosDisponibles[index];
            puntosDisponibles.RemoveAt(index);

            navMeshAgent.SetDestination(destino.position);
            ultimaPosicionFuego = transform.position;

            while (Vector3.Distance(transform.position, destino.position) > 0.5f)
            {
                float distanciaRecorrida = Vector3.Distance(transform.position, ultimaPosicionFuego);
                if (distanciaRecorrida >= distanciaEntreLlamas)
                {
                    Vector3 posicionFuego = transform.position;
                    posicionFuego.y = 0.5f;
                    Vector3 direccionMovimiento = (transform.position - ultimaPosicionFuego).normalized;
                    float angulo = Mathf.Atan2(direccionMovimiento.x, direccionMovimiento.z) * Mathf.Rad2Deg;
                    Quaternion rotacionFuego = Quaternion.Euler(0, angulo + 90f, 0);
                    Instantiate(llamaPrefab, posicionFuego, rotacionFuego);
                    ultimaPosicionFuego = transform.position;
                }
                yield return null;
            }

            animator.SetBool("atacando", false);
            yield return new WaitForSeconds(2f);
            puntosVisitados++;
        }

        // Mover al punto final fijo (EnemigoPuntosMov (5))
        animator.SetBool("atacando", true);
        navMeshAgent.SetDestination(puntoFinal.position);
        ultimaPosicionFuego = transform.position;

        while (Vector3.Distance(transform.position, puntoFinal.position) > 0.5f)
        {
            float distanciaRecorrida = Vector3.Distance(transform.position, ultimaPosicionFuego);
            if (distanciaRecorrida >= distanciaEntreLlamas)
            {
                Vector3 posicionFuego = transform.position;
                posicionFuego.y = 0.5f;
                Vector3 direccionMovimiento = (transform.position - ultimaPosicionFuego).normalized;
                float angulo = Mathf.Atan2(direccionMovimiento.x, direccionMovimiento.z) * Mathf.Rad2Deg;
                Quaternion rotacionFuego = Quaternion.Euler(0, angulo + 90f, 0);
                Instantiate(llamaPrefab, posicionFuego, rotacionFuego);
                ultimaPosicionFuego = transform.position;
            }
            yield return null;
        }

        animator.SetBool("atacando", false);
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }


    private IEnumerator AtaqueRayoFase1()
    {
        isAttacking = true;
        enemigoScript.seguirJugador = false;
        navMeshAgent.isStopped = true; // Detiene completamente el movimiento
        animator.SetBool("atacando_fase1", true);

        // Calcular dirección hacia el jugador
        Vector3 direccion = (jugador.transform.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg;

        // Instanciar el rayo
        GameObject rayo = Instantiate(rayoPrefab, transform.position + Vector3.up * 1.5f, Quaternion.Euler(90f, angulo, 0f));
        rayo.GetComponent<Jefe03_Rayo>().AsignarJefe(transform);
        Transform rayoTransform = rayo.transform;
        Jefe03_Rayo rayoScript = rayo.GetComponent<Jefe03_Rayo>();
        rayoScript.velocidadRotacion = velocidadRayoFase1;
        rayoScript.radio = 3f;
        rayoScript.anguloInicial = angulo;
        rayoScript.girar = true;

        // Esperar hasta que el rayo sea destruido
        while (rayo != null)
        {
            Vector3 rotacionRayo = rayoTransform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotacionRayo.y, 0f); // Solo rota en Y

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("atacando_fase1", false);
        navMeshAgent.isStopped = false; // Reactivar movimiento
        isAttacking = false;
    }

    private IEnumerator AtaqueRayosFase2()
    {
        isAttacking = true;
        animator.SetBool("atacando_fase2", true);
        mirando = true;
        navMeshAgent.isStopped = true; // Detiene completamente el movimiento

        // Crear 4 rayos en direcciones cardinales con ángulos iniciales
        float distanciaDesdeCentro = 3f;

        // Crear 4 rayos en las 4 direcciones cardinales
        Vector3[] direcciones = {
        Vector3.forward,    // Norte (Z+)
        Vector3.right,     // Este (X+)
        Vector3.back,      // Sur (Z-)
        Vector3.left       // Oeste (X-)
        };

        float[] angulosIniciales = { 0f, 90f, 180f, 270f };
        List<GameObject> rayos = new List<GameObject>();

        for (int i = 0; i < 4; i++)
        {
            // Calcular posición del rayo (centro + dirección * distancia)
            Vector3 posicionRayo = transform.position + (direcciones[i] * distanciaDesdeCentro);
            posicionRayo.y = transform.position.y + 1.5f; // Altura del rayo

            // Instanciar el rayo
            GameObject rayo = Instantiate(
                rayoPrefab,
                posicionRayo,
                Quaternion.Euler(90f, angulosIniciales[i], 0f)
            );
            rayo.GetComponent<Jefe03_Rayo>().AsignarJefe(transform);

            // Configurar el rayo
            Jefe03_Rayo rayoScript = rayo.GetComponent<Jefe03_Rayo>();
            rayoScript.velocidadRotacion = velocidadRayoFase2;
            rayoScript.radio = distanciaDesdeCentro;
            rayoScript.anguloInicial = angulosIniciales[i];
            rayoScript.girar = true;
            rayoScript.duracion = 7f;
            rayos.Add(rayo);
        }

        // Esperar mientras los rayos están activos
        yield return new WaitForSeconds(7f);

        // Limpieza
        foreach (GameObject rayo in rayos)
        {
            if (rayo != null) Destroy(rayo);
        }

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("atacando_fase2", false);
        mirando = false;
        navMeshAgent.isStopped = false; // Reactivar movimiento
        isAttacking = false;
    }
}
